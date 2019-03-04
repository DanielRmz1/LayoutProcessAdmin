using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class Question       
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public int Type { get; set; }               
        public List<Answer> Answers { get; set; }
    }
}