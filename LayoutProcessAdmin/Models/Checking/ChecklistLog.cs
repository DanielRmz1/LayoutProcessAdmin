using LayoutProcessAdmin.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class ChecklistLog
    {
        public int Id { get; set; }
        public Checklist Checklist_Id { get; set; }
        public User User_Id { get; set; }  //Usuario que contestó el Checklist
        public DateTime CheckDate { get; set; } //Fecha que se contesto el checklist
        public bool Checked { get; set; }   // Para saber si un checklist fue o no contestado
    }
}