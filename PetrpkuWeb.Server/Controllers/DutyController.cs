﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/duty")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DutyController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("today")]
        public async Task<ActionResult<Duty>> GetWhoIsDutyTodayAsync()
        {
            return await _db.Duties
                .Where(d => d.DayOfDuty.DayOfYear == DateTime.Now.DayOfYear)
                .Include(u => u.AssignedTo)
                .FirstOrDefaultAsync();
        }

        [HttpGet("month/{selectedMonth:int}/{selectedYear:int}")]
        public async Task<ActionResult<List<Duty>>> GetDutyMonthAsync([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            return await _db.Duties
                .Where(d => (d.DayOfDuty.Month == selectedMonth && d.DayOfDuty.Year == selectedYear))
                .Include(u => u.AssignedTo)
                .OrderBy(d => d.DayOfDuty)
                .ToListAsync();
        }

        [HttpGet("getfile/{selectedMonth:int}/{selectedYear:int}")]
        public async Task<ActionResult> GetFileAsync([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            string filePath = $"UploadFiles/{selectedMonth}-{selectedYear}.docx";

            var listOfDuty = await _db.Duties
                .Where(d => (d.DayOfDuty.Month == selectedMonth && d.DayOfDuty.Year == selectedYear))
                .Include(u => u.AssignedTo)
                .OrderBy(d => d.DayOfDuty)
                .ToListAsync();

            var days = Enumerable.Range(1, DateTime.DaysInMonth(selectedYear, selectedMonth)).Select(day => new DateTime(selectedYear, selectedMonth, day))
                .ToList();

            var file = GenerateDocFile(filePath, listOfDuty, days);

            return File(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"API_{DateTime.Now.ToString("dd-MM-yyyy")}.docx");
        }

        [HttpPost("createdutylist")]
        public async Task<ActionResult> PostDutyListAsync([FromBody] List<Duty> dutyList)
        {
            await _db.Duties.AddRangeAsync(dutyList);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Duty>> PostDutyAsync([FromBody]Duty duty)
        {
            _db.Duties.Add(duty);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update/{dutyId:int}")]
        public async Task<IActionResult> PutDutyAsync(int dutyId, Duty duty)
        {
            if (dutyId != duty.DutyId)
            {
                return BadRequest();
            }

            _db.Entry(duty).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{dutyId:int}")]
        public async Task<ActionResult> DeleteDutyAsync([FromRoute] int dutyId)
        {
            var dutyUser = await _db.Duties.FindAsync(dutyId);

            if (dutyUser == null)
            {
                return NotFound();
            }

            _db.Duties.Remove(dutyUser);
            await _db.SaveChangesAsync();
            return NoContent();
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


                    if (listOfDuty.Find(d => d.DayOfDuty.Day == day.Day) != null)
                    {
                        tc2 = new TableCell(new Paragraph(new Run(new Text(listOfDuty.Find(d => d.DayOfDuty.Day == day.Day).AssignedTo.LastName))));
                        tc3 = new TableCell(new Paragraph(new Run(new Text(listOfDuty.Find(d => d.DayOfDuty.Day == day.Day).AssignedTo.WorkingPosition))));
                    }
                    else
                    {
                        tc2 = new TableCell(new Paragraph(new Run(new Text(""))));
                        tc3 = new TableCell(new Paragraph(new Run(new Text(""))));
                    }

                    // Add a cell to each column in the row.
                    tr1.Append(tc1, tc2, tc3);

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