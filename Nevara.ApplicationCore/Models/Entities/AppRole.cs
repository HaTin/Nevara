using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Nevara.ApplicationCore.Models.Interfaces;

namespace Nevara.ApplicationCore.Models.Entities    
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
