using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Auditing
{
    public class LayersTable
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Area { get; set; }
        public string Period { get; set; }
        public string Checklist { get; set; }
    }
}