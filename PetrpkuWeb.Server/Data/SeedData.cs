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

            _db.Attachments.Add(avatar);
            _db.SaveChanges();
        }
    }
}
