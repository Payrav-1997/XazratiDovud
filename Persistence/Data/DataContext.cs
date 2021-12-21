using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{

    public class DataContext : IdentityDbContext<User, Roles, int>
    {
        public DbSet<Advice> Advices { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Qadamgoh> Qadamgohs { get; set; }
        public DbSet<RestZone> RestZones { get; set; }
        public DbSet<RestZoneFiles> RestZoneFiles { get; set; }
        public DbSet<Workshop> Workshops { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
                : base(options)
        {
        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .ToTable("Users");
            builder.Entity<IdentityRole>()
                .ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRoles");
            builder.Entity<IdentityUserToken<int>>()
                .ToTable("UserToken");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

}
