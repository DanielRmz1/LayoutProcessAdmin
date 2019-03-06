using LayoutProcessAdmin.Models.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Checking
{
    [Table("Tbl_ChecklistsLog")]
    public class ChecklistLog
    {
        [Key]
        public int int_IdListLog { get; set; }
        public Checklist int_Checklist { get; set; }
        public User int_User { get; set; }  //Usuario que contestó el Checklist
        public DateTime dte_CheckDate { get; set; } //Fecha que se contesto el checklist
        public bool bit_Checked { get; set; }   // Para saber si un checklist fue o no contestado
    }
}