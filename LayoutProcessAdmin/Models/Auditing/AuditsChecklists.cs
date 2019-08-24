using LayoutProcessAdmin.Models.Checking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace LayoutProcessAdmin.Models.Auditing
{
    [Table("Tbl_AuditsChecklists")]
    [DataContract(IsReference = true)]
    public class AuditsChecklists
    {
        [Key]
        public int int_IdAuditsChecklists { get; set; }

        public AuditConfig AuditConfig { get; set; }

        public Checklist Checklist { get; set; }

    }
}