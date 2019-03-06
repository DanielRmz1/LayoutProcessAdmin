using LayoutProcessAdmin.Models.Checking;
using System;
using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Calendar
{
    public class Event
    {
        [Key]
        public int int_IdEvent { get; set; }
        public Checklist Checklist_Id { get; set; }

        public string chr_Title { get; set; }
        public DateTime dte_ScheduleDate { get; set; }
        public int int_State { get; set; }   //0 -> SCHEDULED, 1 -> NOTANSWERED, ANSWERED -> 2
    }
}