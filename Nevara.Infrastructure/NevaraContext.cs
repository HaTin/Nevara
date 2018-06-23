using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nevara.Data.Entities;

namespace Nevara.Infrastructure
{
    public class NevaraContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public NevaraContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Color> Colors { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region IdentityConfig

            

            #endregion
        }
    }
}
