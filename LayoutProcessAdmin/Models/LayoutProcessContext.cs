using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Calendar;
using LayoutProcessAdmin.Models.Checking;
using System.Data.Entity;

namespace LayoutProcessAdmin.Models
{
    public class LayoutProcessContext : DbContext
    {
        public LayoutProcessContext()
            :base("DefaultConnection")
        {
            
        }

        DbSet<Role> Tbl_LpaRoles { get; set; }
        DbSet<User> Tbl_Users { get; set; }
        DbSet<UserRoles> Tbl_UserRoles { get; set; }
        DbSet<Answer> Tbl_Answers { get; set; }
        DbSet<AnswerLog> Tbl_AnswerLogs { get; set; }
        DbSet<Checklist> Tbl_Checklists { get; set; }
        DbSet<ChecklistLog> Tbl_ChecklistLogs { get; set; }
        DbSet<Period> Tbl_Periods { get; set; }
        DbSet<Question> Tbl_Questions { get; set; }
        DbSet<QuestionLog> Tbl_QuestionsLog { get; set; }
        DbSet<UsersChecklist> Tbl_UsersChecklists { get; set; }
        DbSet<Event> Tbl_Events { get; set; }

    }
}