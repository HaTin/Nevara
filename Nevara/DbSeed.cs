using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nevara.Models.Entities;

namespace Nevara
{
    public class DbSeed
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly NevaraDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public DbSeed(UserManager<AppUser> userManager, NevaraDbContext context, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer"        
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                });
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "ha tin",
                    Email = "admin@gmail.com",
                    Avatar = "/admin-client/assets/images/users/1.jpg"
                },"123456");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!_context.Colors.Any())
            {
                List<Color> colors = new List<Color>()
                {
                    new Color() {ColorName = "Red"},
                    new Color() {ColorName = "Blue"},
                    new Color() {ColorName = "Green"},
                    new Color() {ColorName = "White"}
                };
            }
           
            if (!_context.Collections.Any())
            {
                List<Collection> collections = new List<Collection>()
                {
                    new Collection() {CollectionName = "Autumn", Description = "This is autumn description"},
                    new Collection() {CollectionName = "Fall", Description = "This is autumn description"},
                    new Collection() {CollectionName = "Summner", Description = "This is autumn description"},
                    new Collection() {CollectionName = "Spring", Description = "This is autumn description"},
                };
            }

            if (!_context.Materials.Any())
            {
                List<Material> materials = new List<Material>()
                {
                    new Material() {MaterialName = "Wood"},
                    new Material() {MaterialName = "Plastic"},
                    new Material() {MaterialName = "Wood"},
                };
            }

            if (!_context.Manufacturers.Any())
            {
                List<Manufacturer> manufacturers = new List<Manufacturer>()
                {
                    new Manufacturer() {ManufacturerName = "Yokohama"},
                    new Manufacturer() {ManufacturerName = "Samsung"},
                    new Manufacturer() {ManufacturerName = "Honda"},
                };
            }
            
            if (!_context.Categories.Any())
            {
                List<Category> categories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Beds",Image = "/images/Category/cat3.jpg",
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Bed 1",Thumbnail ="/images/Product/pro1.jpg",CollectionId = 1,ManufacturerId = 1,MaterialId = 1,},
                            new Product(){Name = "Bed 2",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 2,},
                            new Product(){Name = "Bed 3",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 3,},
                        }
                    },
                 
                    new Category()
                    {
                        
                        Name = "Tables",Image = "/images/Category/cat1.jpg",
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Table 1",Thumbnail ="/images/Product/pro1.jpg",CollectionId = 1,ManufacturerId = 1,MaterialId = 1,},
                            new Product(){Name = "Table 2",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 2,},
                            new Product(){Name = "Table 3",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 3,},
                        }
                    },
                    new Category()
                    {
                        Name = "Chair",Image = "/images/Category/cat2.jpg",
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Chair 1",Thumbnail ="/images/Product/pro1.jpg",CollectionId = 1,ManufacturerId = 1,MaterialId = 1,},
                            new Product(){Name = "Chair 2",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 2,},
                            new Product(){Name = "Chair 3",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 3,}
                        }
                    },
                    new Category()
                    {
                        Name = "Kitchen Furniture",Image = "/images/Category/cat2.jpg",
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Kitchen 1 ",Thumbnail ="/images/Product/pro1.jpg",CollectionId = 1,ManufacturerId = 1,MaterialId = 1,},
                            new Product(){Name = "Kitchen 2",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 2,},
                            new Product(){Name = "Kitchen 3",Thumbnail ="/images/Product/pro2.jpg",CollectionId = 1,ManufacturerId = 2,MaterialId = 3,}
                        }
                    }
                };
            }
            //Save Change async
            await _context.SaveChangesAsync();
        }
    }
}

