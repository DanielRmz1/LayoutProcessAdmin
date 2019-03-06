using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Account
{
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

    }
}