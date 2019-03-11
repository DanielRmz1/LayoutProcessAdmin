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
        public string chr_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string chr_Description { get; set; }

        public bool bit_ManageUsers { get; set; }

        public bool bit_ManageGlobalUsers { get; set; }

        public bool bit_ManageChecklist { get; set; }

        public bool bit_FillChecklists { get; set; }
        
        public List<UserRoles> UserRoles { get; set; }

    }
}