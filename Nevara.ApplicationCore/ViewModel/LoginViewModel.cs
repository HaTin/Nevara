using System.ComponentModel.DataAnnotations;

namespace Nevara.ApplicationCore.ViewModel
{
    public class LoginViewModel
    {        
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
 
      
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
