using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Account
{
    [Table("Tbl_UserRoles")]
    public class UserRoles
    {
        [Key]
        public int int_IdUserRoles { get; set; }
        public User int_User { get; set; }
        public Role int_LpaRole { get; set; }
    }
}