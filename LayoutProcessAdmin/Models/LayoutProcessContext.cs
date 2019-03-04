using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Checking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models
{
    public class LayoutProcessContext : DbContext
    {
        public LayoutProcessContext()
            :base("DefaultConnection")
        {
            
        }

        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<AnswerLog> AnswerLogs { get; set; }
        DbSet<Checklist> Checklists { get; set; }
        DbSet<ChecklistLog> ChecklistLogs { get; set; }
        DbSet<Period> Periods { get; set; }
        DbSet<QuestionLog> Question { get; set; }
        DbSet<UsersChecklist> UsersChecklists { get; set; }

    }
}