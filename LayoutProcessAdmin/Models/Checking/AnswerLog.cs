using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Checking
{
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