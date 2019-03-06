﻿
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
        [Display(Name = "User name")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Name")]
        public string chr_Name { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string chr_LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^.*(?=.{6,18})(?=.*\d)(?=.*[A-Za-z])(?=.*[@_/!?#.,]{1,}).*$", ErrorMessage = "The password must contain between 6 and 18 characters, letters, numbers and a special character (, _ ! ? @ # /)")]
        public string chr_Password { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string chr_Email { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string chr_Phone { get; set; }
    }
}