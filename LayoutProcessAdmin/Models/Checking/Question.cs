using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_Questions")]
    public class Question       
    {
        [Key]
        public int int_IdQuestion { get; set; }

        [Required]
        [StringLength(100)]
        public string chr_Description { get; set; }

        /// <summary>
        /// El tipo de la pregunta podra ser:
        /// M -> Multiple -> Respuesta A) B) C) D) (El plan es que sean n respuestas)
        /// A -> Abierta ->  Respuesta: ____________________________________
        /// B -> Si / No ->  Respuesta: Yes) No) o Si) No)
        /// </summary>
        [Required]
        public char int_Type { get; set; } 
    }
}