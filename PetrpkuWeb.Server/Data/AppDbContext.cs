using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PetrpkuWeb.Server.Models;

namespace PetrpkuWeb.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUserIdentity>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SiteSection> SiteSections { get; set; }
        public DbSet<SiteSubsection> SiteSubsections { get; set; }
        public DbSet<Sent> Sents { get; set; }
        public DbSet<Checked> Checkeds { get; set; }
        public DbSet<Published> Publisheds { get; set; }
        public DbSet<CssType> CssTypes { get; set; }
    }
    
}
