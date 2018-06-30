using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>,IHasSoftDelete
    {
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }    
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
