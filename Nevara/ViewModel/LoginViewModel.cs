using System;
using System.ComponentModel.DataAnnotations;

namespace Nevara.ViewModel
{
    public class LoginViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy:0}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
