using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public class AppIdentityService : IAppIdentityService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUserIdentity> _userManager;

        public AppIdentityService(
            AppDbContext db,
            UserManager<AppUserIdentity> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<AppUserIdentity>> GetAllIdentityUsersOrderById()
        {
            return await _db.Users
                .Include(u => u.AssosiatedUser)
                .OrderBy(u => u.AppUserId)
                .ToListAsync();
        }

        public async Task<List<AppUserIdentity>> GetAllIdentityUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<AppUserIdentity> AddIdentityUser(IAuthUser authUser)
        {

            var appUserIdentity = new AppUserIdentity()
            {
                UserName = authUser.UserName,
                NormalizedUserName = authUser.UserName.ToUpperInvariant(),
                Email = authUser.Email,
                NormalizedEmail = authUser.Email.ToUpperInvariant(),
                DisplayName = authUser.DisplayName,
                IsActive = true,
                IsLdapUser = true,
                AssosiatedUser = new AppUser()
                {
                    DisplayName = authUser.DisplayName,
                    IsDuty = false
                }
            };

            await _userManager.CreateAsync(appUserIdentity);
            await _userManager.AddToRoleAsync(appUserIdentity, "User");

            return appUserIdentity;
        }

        public async Task<AppUserIdentity> FindByName(IAuthUser authUser)
        {
            return await _userManager.FindByNameAsync(authUser.UserName);
        }

        public async Task UpdateEmail(AppUserIdentity appUserIdentity, IAuthUser authUser)
        {
            // update email if changed
            if (appUserIdentity.Email != authUser.Email)
            {
                appUserIdentity.Email = authUser.Email;
                appUserIdentity.NormalizedEmail = authUser.Email.ToUpperInvariant();

                await _userManager.UpdateAsync(appUserIdentity);
            }
        }
    }
}
