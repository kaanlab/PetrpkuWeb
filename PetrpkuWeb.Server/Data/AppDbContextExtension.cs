using Microsoft.AspNetCore.Identity;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Extensions;

namespace PetrpkuWeb.Server.Data
{
    public static class AppDbContextExtension
    {
        //public static UserManager<AppUser> UserManager { get; set; }

        public static void EnsureSeeded(this AppDbContext context, UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").GetAwaiter().GetResult() is null)
            {
                var appUser = new AppUser()
                {
                    Avatar = @"/img/user/default_avatar.png",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    DisplayName = "Администратор",
                    Email = "admin@petrpku.ru",
                    NormalizedEmail = "ADMIN@PETRPKU.RU",
                    IsActive = true,
                    IsDuty = false,
                    LdapAuth = false
                };

                userManager.CreateAsync(appUser, "P@ssword1").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(appUser, AuthRoles.ADMIN).GetAwaiter().GetResult();

            }
        }
    }
}
