using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class Checklist
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
        
        [Required]
        public Period Period_Id { get; set; }
    }
}