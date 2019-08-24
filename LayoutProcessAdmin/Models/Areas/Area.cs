using LayoutProcessAdmin.Models.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Areas
{
    [Table("Tbl_Areas")]
    public class Area
    {
        [Key]
        public int int_IdArea { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Index(IsUnique = true)]
        [DataType(DataType.Text)]
        [Display(Name = "Key")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public Machine int_IdMach { get; set; }

        public Group int_IdGroup { get; set; }

        public List<AuditConfig> AuditConfigs { get; set; }
    }
}