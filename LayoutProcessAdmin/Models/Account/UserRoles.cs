using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Account
{
    public class UserRoles
    {
        [Key]
        public int int_IdUserRoles { get; set; }
        public User int_User { get; set; }
        public Role int_LpaRole { get; set; }
    }
}