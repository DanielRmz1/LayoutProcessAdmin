using System.ComponentModel.DataAnnotations;

namespace LayoutProcessAdmin.Models.Checking
{
    public class QuestionLog
    {
        [Key]
        public int int_IdQuestionLog { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public AnswerLog Answers { get; set; }
    }
}