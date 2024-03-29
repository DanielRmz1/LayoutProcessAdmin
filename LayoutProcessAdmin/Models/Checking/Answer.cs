﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Answers")]
    public class Answer
    {
        [Key]
        public int int_IdAnswer { get; set; }
        
        [StringLength(50)]
        public string chr_Description { get; set; }
        
        public string chr_Variable { get; set; }

        public double dbl_LowerLimit { get; set; }
        public double dbl_UpperLimit { get; set; }

        public Question int_Question { get; set; }


    }
}