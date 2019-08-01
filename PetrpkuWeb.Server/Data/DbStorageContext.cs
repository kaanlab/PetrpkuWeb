using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class DbStorageContext : DbContext
    {
        public DbStorageContext()
        {
        }

        public DbStorageContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Duty> Duties { get; set; }
        
    }
}
