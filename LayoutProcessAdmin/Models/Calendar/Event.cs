using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Auditing;
using LayoutProcessAdmin.Models.Checking;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Calendar
{
    [Table("Tbl_Events")]
    public class Event
    {
        [Key]
        public int int_IdEvent { get; set; }
        //public Checklist Checklist_Id { get; set; }

        public AuditConfig AuditConfig { get; set; }

        public string chr_Title { get; set; }

        public DateTime dte_ScheduleDate { get; set; }

        /// <summary>
        ///  0 -> SCHEDULED (Evento o checklist pendiente)
        ///  1 -> NOSTANSWERED (Es cuando el evento ha caducado y el checklist no se contestó)
        ///  2 -> Answered (El checklist se contestó)
        ///  3 -> PUEDE SER CONTESTADO O ATENDIDO EN CUALQUIER MOMENTO
        /// </summary>
        public int int_State { get; set; }

        public User User { get; set; }
    }
}