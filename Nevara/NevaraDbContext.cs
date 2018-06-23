using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EfCore.Shaman;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Nevara.Models.Entities;

namespace Nevara
{
    public class NevaraDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public NevaraDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new {x.RoleId, x.UserId});                             
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });
            this.FixOnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }


    }
  
}
