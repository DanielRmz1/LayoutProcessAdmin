using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Areas
{
    [Table("Tbl_Groups")]
    public class Group
    {
        [Key]
        public int int_IdGroup { get; set; }

        [StringLength(35)]
        [Index(IsUnique = true)]
        public string chr_Key { get; set; }

        [StringLength(50)]
        public string chr_Description { get; set; }
    }
}