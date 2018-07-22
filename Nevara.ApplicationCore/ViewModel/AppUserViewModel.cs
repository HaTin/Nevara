using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nevara.ApplicationCore.ViewModel
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

        //private List<OrderViewModel> OrderViewModels { get; set; }
    }
}
