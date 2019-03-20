using LayoutProcessAdmin.Models.Account;
using System.Collections.Generic;
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
        [StringLength(20)]
        [Display(Name = "Code")]
        [Index(IsUnique = true)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed in code field.")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string chr_Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }
        
        /// <summary>
        ///  d for Day
        ///  m for Month
        ///  w for Week
        ///  q for quincena
        /// </summary>
        [Display(Name = "Period")]
        public Period int_Period { get; set; }

        [NotMapped]
        public string SelectedPeriod { get; set; }

        [NotMapped]
        public string[] Days { get; set; }

        [Display(Name = "Active")]
        public bool bit_Activo { get; set; }

        public User int_Owner { get; set; }

        public List<Question> Questions { get; set; }
    }
}