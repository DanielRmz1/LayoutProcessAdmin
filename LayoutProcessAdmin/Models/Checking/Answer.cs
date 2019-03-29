using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Answers")]
    public class Answer
    {
        [Key]
        public int int_IdAnswer { get; set; }

        [Required]
        [StringLength(50)]
        public string chr_Description { get; set; }

        public string chr_Entry { get; set; }
        public bool bit_Selected { get; set; }

        public Question int_Question { get; set; }
    }
}