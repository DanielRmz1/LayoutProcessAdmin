using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Account
{
    [Table("Tbl_LpaRoles")]
    public class Role
    {
        [Key]
        public int int_IdRole { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Role Name")]
        public string chr_Name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }

        [Display(Name = "Users")]
        public bool bit_ManageUsers { get; set; }

        [Display(Name = "Global Users")]
        public bool bit_ManageGlobalUsers { get; set; }

        [Display(Name = "Checklists")]
        public bool bit_ManageChecklist { get; set; }

        [Display(Name = "Answer Checlists")]
        public bool bit_FillChecklists { get; set; }
        
        public List<UserRoles> UserRoles { get; set; }

    }
}