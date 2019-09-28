using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class SeedData
    {
        public static void Initialize(AppDbContext _db)
        {
            var avatar = new Attachment()
            {
                Name = "default_avatar.png",
                Extension = ".png",
                Path = @"/img/user/default_avatar.png"
            };
            //_db.Attachments.Add(avatar);

            var building = new Building()
            {
                Name = "пусто"
            };

            var departmaent = new Department()
            {
                Name = "пусто"
            };

            //_db.SaveChanges();

            var appUserIdentity = new AppUserIdentity()
            {
                UserName = "icer",
                DisplayName = "Кантышев А.В.",
                AssosiateUser = new AppUser()
                {
                    DisplayName = "Кантышев А.В.",
                    Avatar = avatar,
                    Building = building,
                    Department = departmaent,
                    IsActive = true,
                    IsDuty = false
                }
            };

            _db.Users.Add(appUserIdentity);
            _db.SaveChanges();
        }
    }
}
