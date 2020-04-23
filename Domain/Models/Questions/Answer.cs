using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Questions
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
        public Answer()
        {

        }
    }
}
