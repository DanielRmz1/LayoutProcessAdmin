using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class Period
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RepeatPeriod { get; set; }
        public List<string> Days { get; set; }
    }
}