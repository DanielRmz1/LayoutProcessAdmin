using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Areas
{
    [Table("Tbl_Groups")]
    public partial class Group
    {
        [Key]
        public int int_IdGroup { get; set; }

        [Required]
        [StringLength(35)]
        public string chr_Key { get; set; }

        [StringLength(50)]
        public string chr_Description { get; set; }

        [StringLength(20)]
        public string chr_Icon { get; set; }

        [StringLength(10)]
        public string chr_Color { get; set; }

        [Required]
        [StringLength(1)]
        public string chr_Status { get; set; }

        public bool bit_Active { get; set; }

        [StringLength(30)]
        public string chr_Reference { get; set; }

        public List<Area> Areas { get; set; }
    }
}
