using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetrpkuWeb.Shared.Models;

namespace PetrpkuWeb.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUserIdentity>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Duty> Duties { get; set; }
    }
    
}
