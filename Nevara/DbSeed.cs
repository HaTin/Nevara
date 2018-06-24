using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nevara.Models.Entities;

namespace Nevara
{
    public class DbSeed
    {
        private UserManager<AppUser> _userManager;
        private readonly NevaraDbContext _context;
        private RoleManager<AppRole> _roleManager;

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
            //Save Change async
            await _context.SaveChangesAsync();
        }
    }
}

