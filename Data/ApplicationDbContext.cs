using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<PortFolio> PortFolios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PortFolio>(x => x.HasKey(p => new {p.AppUserId , p.StockId}));

            builder.Entity<PortFolio>()
                .HasOne(u => u.AppUser)
                .WithMany( u => u.PortFolio)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<PortFolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.portFolio)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> Roles = new List<IdentityRole>
            {
               new IdentityRole{
                  Name = "admin",
                  NormalizedName = "ADMIN"
               },
               new IdentityRole
               {
                 Name = "user",
                 NormalizedName = "USER"
               } 
            };

            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}