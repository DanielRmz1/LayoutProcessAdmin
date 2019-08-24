using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Checklist")]
    [DataContract(IsReference = true)]
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

        [StringLength(350)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }
        
        public User int_Owner { get; set; }

        public List<Question> Questions { get; set; }

        public List<AuditConfig> AuditConfigs { get; set; }
    }
}