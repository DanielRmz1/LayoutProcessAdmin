using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Account
{
    public class Login
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        public string Chr_Clave { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password:")]
        public string Chr_Password { get; set; }
    }
}