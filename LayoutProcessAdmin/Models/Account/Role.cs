using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Account
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool ManageUsers { get; set; }
        public bool ManageChecklist { get; set; }
        public bool FillChecklists { get; set; }
    }
}