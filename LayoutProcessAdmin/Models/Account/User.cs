
using LayoutProcessAdmin.Models.Auditing;
using LayoutProcessAdmin.Models.Calendar;
using LayoutProcessAdmin.Models.Checking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        [Display(Name = "Username")]
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
        [RegularExpression(@"^.*(?=.{6,18})(?=.*\d)(?=.*[A-Za-z])(?=.*[@_/!?#.,-]{1,}).*$", ErrorMessage = "The password must contain between 6 and 18 characters, letters, numbers and a special character (, _ - ! ? @ # /)")]
        public string chr_Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [RegularExpression(@"^.*(?=.{6,18})(?=.*\d)(?=.*[A-Za-z])(?=.*[@_/!?#.,-]{1,}).*$", ErrorMessage = "The password must contain between 6 and 18 characters, letters, numbers and a special character (, _ - ! ? @ # /)")]
        [NotMapped]
        public string chr_ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string chr_Email { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string chr_Phone { get; set; }
        
        public List<Checklist> Checklists { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Select a Role")]
        public int UserRole { get; set; }

        [Display(Name = "Role")]
        public List<UserRoles> UserRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public List<UsersAudits> UsersChecklists { get; set; }
        
        public List<Event> Events { get; set; }
    }
}