using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class AppUsersService : IAppUsersService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUsersService(
            AppDbContext db,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<AppUser>> GetAllOrderById()
        {
            return await _db.Users
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<AppUser> Add(IAuthUser authUser)
        {

            var appUser = new AppUser()
            {
                UserName = authUser.UserName,
                NormalizedUserName = authUser.UserName.ToUpperInvariant(),
                Email = authUser.Email,
                NormalizedEmail = authUser.Email.ToUpperInvariant(),
                DisplayName = authUser.DisplayName,
                IsActive = true,
                LdapAuth = true,
                IsDuty = false
            };

            await _userManager.CreateAsync(appUser);
            await _userManager.AddToRoleAsync(appUser, "User");

            return appUser;
        }

        public async Task UpdateEmail(AppUser appUser, IAuthUser authUser)
        {
            appUser.Email = authUser.Email;
            appUser.NormalizedEmail = authUser.Email.ToUpperInvariant();

            await _userManager.UpdateAsync(appUser);
        }

        public async Task<List<AppUser>> GetAllActiveOrderByDispalyName()
        {
            return await _db.Users
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Where(u => u.IsActive)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAllActiveDutiesOrderByDispalyName()
        {
            return await _db.Users
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Where(u => u.IsActive && u.IsDuty)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAllOrderByDispalyName()
        {
            return await _db.Users
                .Include(b => b.Building)
                .Include(d => d.Department)
                .OrderBy(u => u.DisplayName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AppUser> GetById(string appUserId)
        {
            return await _db.Users
                .Include(d => d.Duties)
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Include(a => a.Posts)
                    .ThenInclude(atach => atach.Attachments)
                .Include(a => a.Notes)
                    .ThenInclude(ct => ct.CssType)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == appUserId);
        }

        public async Task<List<AppUser>> GetBirthdaysForOneWeek(DateTime firstDayOfWeek, DateTime lastDayOfWeek)
        {
            return await _db.Users
                .Include(d => d.Department)
                .Where(d => (d.Birthday.DayOfYear >= firstDayOfWeek.DayOfYear && d.Birthday.DayOfYear <= lastDayOfWeek.DayOfYear))
                .OrderBy(o => o.Birthday.DayOfYear)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AppUser> FindById(string appUserId)
        {
            return await _userManager.FindByIdAsync(appUserId);
        }

        public async Task<bool> Update(AppUser appUser)
        {
            var updated = await _userManager.UpdateAsync(appUser);

            return updated.Succeeded;
        }

        public async Task<bool> AddToRole(AppUser appUser, string appRole)
        {
            var added = await _userManager.AddToRoleAsync(appUser, appRole);

            return added.Succeeded;
        }

        public async Task<bool> RemoveFromRole(AppUser appUser, string appRole)
        {
            var removed = await _userManager.RemoveFromRoleAsync(appUser, appRole);

            return removed.Succeeded;
        }

        public IList<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}
