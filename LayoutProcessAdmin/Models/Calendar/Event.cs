using LayoutProcessAdmin.Models.Checking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Calendar
{
    public class Event
    {
        public Checklist Checklist_Id { get; set; }
        public string title { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public int State { get; set; }   //0 -> SCHEDULED, 1 -> NOTANSWERED, ANSWERED -> 2
    }
}