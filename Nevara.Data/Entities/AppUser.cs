using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Nevara.Data.Enum;
using Nevara.Data.Interfaces;

namespace Nevara.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>,ISwitchable
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }       
    }
}
