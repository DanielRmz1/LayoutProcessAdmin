using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Areas
{
    [Table("Tbl_Machines")]
    public class Machine
    {
        [Key]
        public int int_IdMach { get; set; }
        
        [StringLength(35)]
        [Index(IsUnique = true)]
        public string chr_Key { get; set; }

        [StringLength(50)]
        public string chr_Description { get; set; }

        public List<Area> Areas { get; set; }
    }
}