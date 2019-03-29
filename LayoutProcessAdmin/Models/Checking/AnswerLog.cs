using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_AnswersLog")]
    public class AnswerLog
    {
        [Key]
        public int int_IdAnswerLog { get; set; }

        [StringLength(100)]
        public string chr_Description { get; set; }

        [Required]
        public string Entry { get; set; } //Entrada que se dio a la respuesta

        public bool Selected { get; set; }
    }
}