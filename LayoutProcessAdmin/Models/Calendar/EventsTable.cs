using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Calendar
{
    public class EventsTable
    {
        public string Audit { get; set; }

        public string Checklist { get; set; }

        public int State { get; set; }

        public string Area { get; set; }

        public DateTime Date { get; set; }

        public int EventId { get; set; }
        
    }
}