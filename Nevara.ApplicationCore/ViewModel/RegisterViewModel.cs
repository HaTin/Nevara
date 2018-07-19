using System;
using System.ComponentModel.DataAnnotations;

namespace Nevara.ApplicationCore.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name required", AllowEmptyStrings = false)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirm { get; set; }
    }
}
