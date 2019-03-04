using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public int Description { get; set; }

        public string Entry { get; set; }
        public bool Selected { get; set; }
    }
}