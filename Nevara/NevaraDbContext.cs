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
    public class NevaraDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public NevaraDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*builder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Manufacturer>().HasQueryFilter(p => !p.IsDeleted);            
            builder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Collection>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<AppUser>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Material>().HasQueryFilter(p => !p.IsDeleted);*/


            #region seed data
            builder.Entity<Color>().HasData(
                new Color() { Id = 1, ColorName = "Red", Code = "#d80000", IsDeleted = false },
                new Color() { Id = 2, ColorName = "Blue", Code = "#0099cc", IsDeleted = false },
                new Color() { Id = 3, ColorName = "Green", Code = "#29ab87", IsDeleted = false },
                new Color() { Id = 4, ColorName = "White", Code = "#000000", IsDeleted = false }
            );

            builder.Entity<Collection>().HasData(new Collection()
            {
                Id = 1,
                CollectionName = "Autumn",
                Description = "This is autumn description",
                IsDeleted = false
            },
                new Collection()
                {
                    Id = 2,
                    CollectionName = "Fall",
                    Description = "This is autumn description",
                    IsDeleted = false
                },
                new Collection()
                {
                    Id = 3,
                    CollectionName = "Summner",
                    Description = "This is autumn description",
                    IsDeleted = false
                },
                new Collection()
                {
                    Id = 4,
                    CollectionName = "Spring",
                    Description = "This is autumn description",
                    IsDeleted = false
                });
            builder.Entity<Material>().HasData(
                new Material() { Id = 1, MaterialName = "Wood", IsDeleted = false },
                new Material() { Id = 2, MaterialName = "Plastic", IsDeleted = false },
                new Material() { Id = 3, MaterialName = "Wood", IsDeleted = false }
            );
            builder.Entity<Manufacturer>().HasData(

                new Manufacturer() { Id = 1, ManufacturerName = "Yokohama", IsDeleted = false },
                new Manufacturer() { Id = 2, ManufacturerName = "Samsung", IsDeleted = false },
                new Manufacturer() { Id = 3, ManufacturerName = "Honda", IsDeleted = false }

            );
            builder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Beds",
                    IsDeleted = false,

                },
                new Category()
                {
                    Id = 2,
                    Name = "Tables",
                    IsDeleted = false,
                },
                new Category()
                {
                    Id = 3,
                    Name = "Chair",
                    IsDeleted = false,
                },
                new Category()
                {
                    Id = 4,
                    Name = "Kitchen Furniture",
                    IsDeleted = false,
                }
            );
            builder.Entity<Product>().HasData(
                new Product()
                {
                    ColorId = 1,
                    Id = 1,
                    Name = "Bed 1",
                    Thumbnail = "/images/Product/pro1.jpg",
                    CollectionId = 1,
                    ManufacturerId = 1,
                    MaterialId = 1,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 1

                },
                new Product()
                {
                    ColorId = 1,
                    Id = 2,
                    Name = "Bed 2",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 1,
                    ManufacturerId = 2,
                    MaterialId = 2,
                    Price = 3000,
                    IsDeleted = false,
                    CategoryId = 1,
                    Quantity = 10
                },
                new Product()
                {
                    ColorId = 1,
                    Id = 3,
                    Name = "Bed 3",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 2,
                    ManufacturerId = 2,
                    MaterialId = 3,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 1
                },
                new Product()
                {
                    Quantity = 10,
                    ColorId = 1,
                    Id = 4,
                    Name = "Table 1",
                    Thumbnail = "/images/Product/pro1.jpg",
                    CollectionId = 2,
                    ManufacturerId = 1,
                    MaterialId = 1,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 2
                },
                new Product()
                {
                    Quantity = 20,
                    ColorId = 2,
                    Id = 5,
                    Name = "Table 2",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 3,
                    ManufacturerId = 2,
                    MaterialId = 2,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 2
                },
                new Product()
                {
                    Quantity = 50,
                    ColorId = 3,
                    Id = 6,
                    Name = "Table 3",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 4,
                    ManufacturerId = 2,
                    MaterialId = 3,
                    Price = 1000,
                    IsDeleted = false,
                    CategoryId = 2
                },
                new Product()
                {
                    Quantity = 25,
                    ColorId = 1,
                    Id = 7,
                    Name = "Chair 1",
                    Thumbnail = "/images/Product/pro1.jpg",
                    CollectionId = 4,
                    ManufacturerId = 1,
                    MaterialId = 1,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 3
                },
                new Product()
                {
                    Quantity = 30,
                    ColorId = 2,
                    Id = 8,
                    Name = "Chair 2",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 4,
                    ManufacturerId = 2,
                    MaterialId = 2,
                    Price = 1000,
                    IsDeleted = false,
                    CategoryId = 3
                },
                new Product()
                {
                    ColorId = 3,
                    Id = 9,
                    Name = "Chair 3",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 2,
                    ManufacturerId = 2,
                    MaterialId = 3,
                    Price = 5000,
                    IsDeleted = false,
                    CategoryId = 3
                },
                new Product()
                {
                    ColorId = 4,
                    Id = 10,
                    Name = "Kitchen 1 ",
                    Thumbnail = "/images/Product/pro1.jpg",
                    CollectionId = 3,
                    ManufacturerId = 1,
                    MaterialId = 1,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 4
                },
                new Product()
                {
                    ColorId = 1,
                    Id = 11,
                    Name = "Kitchen 2",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 4,
                    ManufacturerId = 2,
                    MaterialId = 2,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 4

                },
                new Product()
                {
                    ColorId = 2,
                    Id = 12,
                    Name = "Kitchen 3",
                    Thumbnail = "/images/Product/pro2.jpg",
                    CollectionId = 1,
                    ManufacturerId = 2,
                    MaterialId = 3,
                    Price = 2000,
                    IsDeleted = false,
                    CategoryId = 4
                }
                );                     
            #endregion            
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new { x.RoleId, x.UserId });
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
