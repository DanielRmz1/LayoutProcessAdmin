using LayoutProcessAdmin.Models.Areas;
using LayoutProcessAdmin.Models.Checking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Auditing
{
    [Table("Tbl_AuditConfig")]
    public class AuditConfig
    {
        [Key]
        public int int_IdAuditConfig { get; set; }
        
        [Display(Name = "Level")]
        public int int_Level { get; set; }

        /// <summary>
        /// Aqui se guardará la ultima fecha en que se crearón los eventos en base a la configuración de esta auditoria y del checklist,
        /// esto con el fin de que el servicio pueda comparar contra la fecha actual y crear nuevos eventos.
        /// </summary>
        public DateTime dte_LastDateCreated { get; set; }
        
        public Audit Audit { get; set; }

        //[NotMapped]
        //[Display(Name = "Area")]
        //public int int_Area { get; set; }

        public Area Area { get; set; }

        //[NotMapped]
        //[Display(Name = "Who will answer this?")]
        //public int[] SelectedUsers { get; set; }

        public List<UsersAudits> UsersAudits { get; set; }

        public Checklist Checklist { get; set; }

        /// <summary>
        /// No me acuerdo por que existe esta propiedad, la relacion entre checklists y audit configs es muchos a uno, no muchos a muchos
        /// </summary>
        //public List<AuditsChecklists> AuditsChecklists { get; set; }

        //[NotMapped]
        //public int id_Period { get; set; }

        //[NotMapped]
        //public string[] Days { get; set; }

        [Display(Name = "Period")]
        [Column("int_idPeriod")]
        public Period int_Period { get; set; }

        //[NotMapped]
        //public string SelectedPeriod { get; set; }
        
    }
}