using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Account
{
    [Table("Tbl_Users")]
    public class User
    {
        [Key]
        public int int_IdUser { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Index(IsUnique = true)]
        [DataType(DataType.Text)]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_Name { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string chr_Password { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string chr_Email { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string chr_Phone { get; set; }
    }
}