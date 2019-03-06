using LayoutProcessAdmin.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Checking
{
    //Modelo que ayudará a relacionar un checklist con los usuarios que pueden atenderlo
    public class UsersChecklist
    {
        [Key]
        public int int_IdUsersChecklist { get; set; }
        public Checklist Checklist_Id { get; set; }
        public User Users { get; set; }
    }
}