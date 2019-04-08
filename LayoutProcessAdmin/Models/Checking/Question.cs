using System.Collections.Generic;
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
        [StringLength(200)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }

        /// <summary>
        /// El tipo de la pregunta podra ser:
        /// M -> Multiple -> Respuesta A) B) C) D) (El plan es que sean n respuestas)
        /// A -> Abierta ->  Respuesta: ____________________________________
        /// B -> Si / No ->  Respuesta: Yes) No) o Si) No)
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "Type")]
        public string chr_Type { get; set; }

        /// <summary>
        /// Propiedad para configurar si una pregunta tiene solamente una respuesta o 
        /// se pueden elegir mas de 2 respuestas por ejemplo una pregunta de respuesta multiple
        /// con checkbox y si es SingleAnswer sería con radio buttons para solamente elegir una respuesta
        /// </summary>
        [Display(Name = "Single")]
        public bool bit_SingleAnswer { get; set; }

        [Display(Name = "Formula")]
        public string chr_Formula { get; set; }

        public List<Answer> Answers { get; set; }

        public Checklist int_Checklist { get; set; }
    }
}