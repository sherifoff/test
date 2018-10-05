using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RealEstate.Models
{
    public class ApplicationDbContext : IdentityDbContext<User> // : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EstateItem> EstateItems { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<HouseType> HouseTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;" +
                "Initial Catalog = RealEstate; " +
                "Integrated Security = True; " +
                "Connect Timeout = 30; " +
                "Encrypt = False;" +
                "TrustServerCertificate = True; " +
                "ApplicationIntent = ReadWrite; " +
                "MultiSubnetFailover = False");
        }
    }
}

