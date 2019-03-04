using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Account
{
    public class UserRoles
    {
        public int Id { get; set; }
        public User User_Id { get; set; }
        public Role Role_Id { get; set; }
    }
}