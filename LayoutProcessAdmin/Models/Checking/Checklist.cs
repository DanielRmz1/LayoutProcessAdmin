using LayoutProcessAdmin.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Checklist")]
    public class Checklist
    {
        [Key]
        public int int_IdList { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Code")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Name")]
        public string chr_Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }
        
        public Period int_Period { get; set; }

        [Display(Name = "Active")]
        public bool bit_Activo { get; set; }

        public User int_Owner { get; set; }
    }
}