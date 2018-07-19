using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Nevara.Models.Entities;

namespace Nevara.Areas.Admin.Helpers
{
    public class CustomClaimsPrincipleFactory : UserClaimsPrincipalFactory<AppUser,AppRole>
    {   
        public CustomClaimsPrincipleFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);            
            var role = await UserManager.GetRolesAsync(user);
            
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {              
                new Claim("UserId",user.Id.ToString()), 
                new Claim("Email",user.Email),
                new Claim("FullName",user.FullName),
                new Claim("Avatar",user.Avatar??string.Empty),
                new Claim("Roles",string.Join(";",role)),
                new Claim("Address",user.Address),
                new Claim("Phone",user.PhoneNumber),                
            });            
            return principal;
            
        }

    }
}
