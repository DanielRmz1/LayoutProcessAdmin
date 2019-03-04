using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayoutProcessAdmin.Models.Checking
{
    public class QuestionLog
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<AnswerLog> Answers { get; set; }
    }
}