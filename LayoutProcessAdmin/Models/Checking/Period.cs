using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Periods")]
    public class Period
    {
        [Key]
        public int int_IdPeriod { get; set; }

        [StringLength(30)]
        public string chr_Name { get; set; }
        /// <summary>
        /// Daily, weekly, monthly, 15 days
        /// </summary>
        public string chr_RepeatPeriod { get; set; } 

        public bool bit_Mon { get; set; }
        public bool bit_Tue { get; set; }
        public bool bit_Wed { get; set; }
        public bool bit_Thu { get; set; }
        public bool bit_Fri { get; set; }
        public bool bit_Sat { get; set; }
        public bool bit_Sun { get; set; }
    }
}