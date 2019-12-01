using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;

namespace PetrpkuWeb.Server.Services
{
    public class DutyService : IDutyService
    {
        private readonly AppDbContext _db;

        public DutyService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Duty> DutyToday()
        {
            return await _db.Duties
                .Include(u => u.AppUser)
                .ThenInclude(b => b.Department)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.DayOfDuty.DayOfYear == DateTime.Now.DayOfYear);
        }

        public async Task<List<Duty>> DutyMonth(int selectedMonth, int selectedYear)
        {
            return await _db.Duties
               .Where(d => (d.DayOfDuty.Month == selectedMonth && d.DayOfDuty.Year == selectedYear))
               .Include(u => u.AppUser)
                   .ThenInclude(b => b.Department)
               .Include(u => u.AppUser)
               .OrderBy(d => d.DayOfDuty)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<byte[]> GetFileAsync(int selectedMonth, int selectedYear)
        {
            var tempFileName = Path.GetTempFileName();
            var listOfDuty = await DutyMonth(selectedMonth, selectedYear);
            var days = Enumerable.Range(1, DateTime.DaysInMonth(selectedYear, selectedMonth)).Select(day => new DateTime(selectedYear, selectedMonth, day))
                .ToList();

           return GenerateDocFile(tempFileName, listOfDuty, days);
        }

        public async Task<List<Duty>> CreateDutys(List<Duty> dutys)
        {
            await _db.Duties.AddRangeAsync(dutys);
            await _db.SaveChangesAsync();

            return dutys;
        }

        public async Task<bool> Create(Duty duty)
        {
            _db.Duties.Add(duty);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Update(Duty duty)
        {
            _db.Attach(duty).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> Delete(int dutyId)
        {
            var duty = await _db.Duties.SingleOrDefaultAsync(d => d.DutyId == dutyId);

            if (duty is null)
                return false;

            _db.Duties.Remove(duty);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        private byte[] GenerateDocFile(string filePath, List<Duty> listOfDuty, List<DateTime> days)
        {
            // Open a WordprocessingDocument for editing using the filepath.
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordprocessingDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();

                Body body = mainPart.Document.AppendChild(new Body());

                // Create a table.
                Table tbl = new Table();

                // Set the style and width for the table.
                TableProperties tableProp = new TableProperties();
                TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };

                // Make the table width 100% of the page width.
                TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                // Apply
                tableProp.Append(tableStyle, tableWidth);
                tbl.AppendChild(tableProp);

                // Add 3 columns to the table.
                TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn());
                tbl.AppendChild(tg);

                // Create 1 row to the table.

                foreach (var day in days)
                {
                    TableRow tr1 = new TableRow();

                    TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(day.Day.ToString()))));
                    TableCell tc2 = new TableCell();
                    TableCell tc3 = new TableCell();
                    TableCell tc4 = new TableCell();
                    TableCell tc5 = new TableCell();

                    if (listOfDuty.Find(d => d.DayOfDuty.Day == day.Day) != null)
                    {
                        var duty = listOfDuty.Find(d => d.DayOfDuty.Day == day.Day);
                        tc2 = new TableCell(new Paragraph(new Run(new Text($"{duty.AppUser.LastName} {duty.AppUser.FirstName} {duty.AppUser.MidleName}"))));
                        tc3 = new TableCell(new Paragraph(new Run(new Text(duty.AppUser.Department.Name))));
                        tc4 = new TableCell(new Paragraph(new Run(new Text(duty.AppUser.WorkingPosition))));
                        tc5 = new TableCell(new Paragraph(new Run(new Text(duty.AppUser.MobPhone))));
                    }
                    else
                    {
                        tc2 = new TableCell(new Paragraph(new Run(new Text(""))));
                        tc3 = new TableCell(new Paragraph(new Run(new Text(""))));
                        tc4 = new TableCell(new Paragraph(new Run(new Text(""))));
                        tc5 = new TableCell(new Paragraph(new Run(new Text(""))));
                    }

                    // Add a cell to each column in the row.
                    tr1.Append(tc1, tc2, tc3, tc4, tc5);

                    // Add row to the table.
                    tbl.AppendChild(tr1);
                }

                // Add the table to the document
                body.AppendChild(tbl);
            }

            var file = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);

            return file;
        }
    }
}
