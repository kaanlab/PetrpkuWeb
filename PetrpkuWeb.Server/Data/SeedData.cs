using Microsoft.AspNetCore.Identity;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class SeedData
    {
        public static async Task Initialize(UserManager<AppUser> userManager)
        {
            var appUser = new AppUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                DisplayName = "Администратор",
                Email = "admin@petrpku.ru",
                NormalizedEmail = "ADMIN@PETRPKU.RU",
                IsActive = true,
                IsDuty = false,
                LdapAuth = false
            };

            var result = await userManager.CreateAsync(appUser, "passw0rd!");
            
            if(result.Succeeded)
                await userManager.AddToRoleAsync(appUser, AuthRoles.ADMIN);
        }
    }
}
