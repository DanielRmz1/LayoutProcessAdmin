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
        /// <summary>
        /// Propiedad para almacenar lo que dio entrada el usuario al contestar el checklist, será mas usado en caso de la pregunta de tipo abierto y el resultado del campo calculado
        /// </summary>
        [Required]
        public string Entry { get; set; } 

        /// <summary>
        /// Para guardar si la pregunta fue o no seleccionada, mas para el tipo multiple y el Yes/No
        /// </summary>
        public bool Selected { get; set; }
    }
}