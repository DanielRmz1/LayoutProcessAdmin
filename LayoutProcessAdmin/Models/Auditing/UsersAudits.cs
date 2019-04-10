using LayoutProcessAdmin.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Auditing
{
    //Modelo que ayudará a relacionar un checklist con los usuarios que pueden atenderlo
    [Table("Tbl_UsersAudits")]
    public class UsersAudits
    {
        [Key]
        public int int_IdUsersChecklist { get; set; }
        public AuditConfig Checklist { get; set; }
        public User User { get; set; }
    }
}