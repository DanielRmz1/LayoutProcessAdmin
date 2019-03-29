using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin
{
    public partial class Part
    {
        [Key]
        public int int_IdPart { get; set; }

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
    }
}
