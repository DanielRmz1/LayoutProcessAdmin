using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Account
{
    public class User
    {
        [Key]
        public int int_IdUser { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_Name { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_Email { get; set; }

        [Required]
        [StringLength(10)]
        public string chr_Phone { get; set; }
    }
}