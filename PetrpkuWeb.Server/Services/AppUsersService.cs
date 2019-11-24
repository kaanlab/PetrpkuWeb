using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class AppUsersService : IAppUsersService
    {
        private readonly AppDbContext _db;

        public AppUsersService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<AppUser>> GetAllActiveOrderByDispalyName()
        {
            return await _db.AppUsers
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Include(ai => ai.AuthIdentity)
                .Where(ai => ai.AuthIdentity.IsActive)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAllActiveDutiesOrderByDispalyName()
        {
            return await _db.AppUsers
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Include(ai => ai.AuthIdentity)
                .Where(ai => ai.AuthIdentity.IsActive)
                .Where(u => u.IsDuty)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAllOrderByDispalyName()
        {
            return await _db.AppUsers
                .Include(b => b.Building)
                .Include(d => d.Department)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AppUser> GetUserById(int appUserId)
        {
            return await _db.AppUsers
                .Include(d => d.DaysOfDuty)
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Include(a => a.Articles)
                    .ThenInclude(atach => atach.Attachments)
                .Include(a => a.Articles)
                    .ThenInclude(ct => ct.CssType)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.AppUserId == appUserId);
        }

        public async Task<List<AppUser>> GetBirthdaysForOneWeek(DateTime firstDayOfWeek, DateTime lastDayOfWeek)
        {
            return await _db.AppUsers
                .Include(d => d.Department)
                .Where(d => (d.Birthday.DayOfYear >= firstDayOfWeek.DayOfYear && d.Birthday.DayOfYear <= lastDayOfWeek.DayOfYear))
                .OrderBy(o => o.Birthday.DayOfYear)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(AppUser appUser)
        {
            _db.AppUsers.Update(appUser);
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}
