using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class MilRequestService : IMilRequestService
    {
        private readonly AppDbContext _db;
        public MilRequestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<MilRequest>> GetAll()
        {
            return await _db.MilRequests
                .Include(s => s.Section)
                .Include(s => s.Subsection)
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(ch => ch.Checked)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(s => s.Sent)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(p => p.Published)
                    .ThenInclude(u => u.AssosiatedUser)
                .OrderByDescending(d => d.CreateDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Create(MilRequest milRequest)
        {
            await _db.MilRequests.AddAsync(milRequest);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<MilRequest> GetOne(int milRequestId)
        {
            return await _db.MilRequests
                .Include(s => s.Section)
                .Include(s => s.Subsection)
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(ch => ch.Checked)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(s => s.Sent)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(p => p.Published)
                    .ThenInclude(u => u.AssosiatedUser)
                .OrderByDescending(d => d.CreateDate)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.MilRequestId == milRequestId);
        }

    }
}
