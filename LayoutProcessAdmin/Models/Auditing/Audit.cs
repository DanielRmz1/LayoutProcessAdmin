using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Auditing
{
    [Table("Tbl_Audit")]
    public class Audit
    {
        [Key]
        public int int_IdAudit { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Name")]
        public string chr_Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }

        /// <summary>
        /// Description: 
        /// Audit -> 0
        /// Encuesta -> 1
        /// Mas tipos proximamente xD
        /// </summary>
        [Display(Name = "Type")]
        public int int_Type { get; set; }

        [Display(Name = "Active")]
        public bool bit_Activo { get; set; }

        public List<AuditConfig> AuditConfigs { get; set; }
    }
}