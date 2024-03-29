﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Periods")]
    public class Period
    {
        [Key]
        public int int_IdPeriod { get; set; }
        
        /// <summary>
        /// Daily = d, weekly = w, monthly = m, 15 days = q
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