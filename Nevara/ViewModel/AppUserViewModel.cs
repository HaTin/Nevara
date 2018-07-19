using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.ViewModel
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{9,12}$",ErrorMessage ="Please input valid phone number")]
        public string Phone { get; set; }
    }
}
