using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Auditing
{
    [Table("Tbl_Periods")]
    public class Period
    {
        [Key]
        public int int_IdPeriod { get; set; }

        /// <summary>
        ///  o for Once
        ///  d for Day
        ///  w for Week
        ///  q for quincena
        ///  m for Month
        /// </summary>
        [Display(Name = "Periodo")]
        public string chr_RepeatPeriod { get; set; } 

        public bool bit_Mon { get; set; }
        public bool bit_Tue { get; set; }
        public bool bit_Wed { get; set; }
        public bool bit_Thu { get; set; }
        public bool bit_Fri { get; set; }
        public bool bit_Sat { get; set; }
        public bool bit_Sun { get; set; }

        public List<AuditConfig> AuditConfigs { get; set; }

    }
}