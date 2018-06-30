using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Nevara.Data.Enum;
using Nevara.Data.Interfaces;

namespace Nevara.Data.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>, ISwitchable
    {
        public AppRole() : base()
        {

        }
        public Status Status { get; set; }
    }
}
