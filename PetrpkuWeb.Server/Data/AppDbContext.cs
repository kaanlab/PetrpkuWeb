using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PetrpkuWeb.Server.Models;
using Microsoft.AspNetCore.Identity;
using PetrpkuWeb.Shared.Extensions;

namespace PetrpkuWeb.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<MilRequest> MilRequests { get; set; }
        public DbSet<SiteSection> SiteSections { get; set; }
        public DbSet<SiteSubsection> SiteSubsections { get; set; }
        public DbSet<Sent> Sents { get; set; }
        public DbSet<Checked> Checkeds { get; set; }
        public DbSet<Published> Publisheds { get; set; }
        public DbSet<CssType> CssTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = AuthRoles.USER, NormalizedName = AuthRoles.USER.ToUpperInvariant(), Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = AuthRoles.ADMIN, NormalizedName = AuthRoles.ADMIN.ToUpperInvariant(), Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = AuthRoles.KADRY, NormalizedName = AuthRoles.KADRY.ToUpperInvariant(), Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>()
                   .HasData(new IdentityRole { Name = AuthRoles.PUBLISHER, NormalizedName = AuthRoles.PUBLISHER.ToUpperInvariant(), Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }
    }
    
}
