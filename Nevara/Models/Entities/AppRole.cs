using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Nevara.Models.Interfaces;
using StackExchange.Redis;

namespace Nevara.Models.Entities    
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>,IHasSoftDelete
    {
        public AppRole() : base()
        {

        }

        public bool IsDeleted { get; set; }
    }
}
