using LayoutProcessAdmin.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    //Modelo que ayudará a relacionar un checklist con los usuarios que pueden atenderlo
    public class UsersChecklist
    {
        public Checklist Checklist_Id { get; set; }
        public List<User> Users { get; set; }
    }
}