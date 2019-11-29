using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class NoteService : ITypeService<Note>
    {
        private readonly AppDbContext _db;

        public NoteService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Note note)
        {
            await _db.Notes.AddAsync(note);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> Delete(int noteId)
        {
            var note = await _db.Notes.SingleOrDefaultAsync(n => n.NoteId == noteId);

            if (note is null)
                return false;

            _db.Notes.Remove(note);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<Note>> GetAll()
        {
            return await _db.Notes
                .Include(a => a.Author)
                .Include(ct => ct.CssType)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Note> GetOne(int noteId)
        {
            return await _db.Notes
                .Include(a => a.Author)
                .Include(ct => ct.CssType)
                .AsNoTracking()
                .SingleOrDefaultAsync(n => n.NoteId == noteId);
        }

        public async Task<bool> Update(Note note)
        {
            _db.Attach(note).State = EntityState.Modified;
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}
