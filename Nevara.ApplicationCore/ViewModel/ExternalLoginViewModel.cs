using System.ComponentModel.DataAnnotations;

namespace Nevara.ApplicationCore.ViewModel
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required] 
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}
