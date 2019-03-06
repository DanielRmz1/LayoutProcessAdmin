using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class Checklist
    {
        [Key]
        public int int_IdList { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(30)]
        public string chr_Name { get; set; }

        [StringLength(100)]
        public string chr_Description { get; set; }
        
        public Period int_Period { get; set; }

        public bool bit_Activo { get; set; }
    }
}