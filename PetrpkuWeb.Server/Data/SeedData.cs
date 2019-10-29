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
            //var building = new Building()
            //{
            //    Name = "отсутствует",
            //    //IsHidden = true
            //};

            //var department = new Department()
            //{
            //    Name = "отсутствует",
            //    //IsHidden = true
            //};

            var appUserIdentity = new AppUserIdentity()
            {
                UserName = "icer",
                NormalizedUserName = "ICER",
                DisplayName = "Кантышев А.В.",
                AssosiatedUser = new AppUser()
                {
                    DisplayName = "Кантышев А.В.",
                    //Building = building,
                    //Department = department,
                    IsActive = true,
                    IsDuty = false
                }
            };

            _db.Users.Add(appUserIdentity);
            _db.SaveChanges();
        }
    }
}
