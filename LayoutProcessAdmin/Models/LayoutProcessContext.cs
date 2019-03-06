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

        DbSet<Role> LpaRoles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<AnswerLog> AnswerLogs { get; set; }
        DbSet<Checklist> Checklists { get; set; }
        DbSet<ChecklistLog> ChecklistLogs { get; set; }
        DbSet<Period> Periods { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<QuestionLog> QuestionsLog { get; set; }
        DbSet<UsersChecklist> UsersChecklists { get; set; }
        DbSet<Event> Events { get; set; }

    }
}