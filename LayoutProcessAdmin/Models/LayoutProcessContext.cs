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

        public DbSet<Role> LpaRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerLog> AnswerLogs { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistLog> ChecklistLogs { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionLog> QuestionsLog { get; set; }
        public DbSet<UsersChecklist> UsersChecklists { get; set; }
        public DbSet<Event> Events { get; set; }
        
    }
}