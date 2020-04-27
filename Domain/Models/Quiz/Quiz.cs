using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models.Questions;
namespace Domain.Models.Quiz
{
    public class Quiz
    {
        public int ID { get; set; }
        public QuizType QuizType { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public int Minutes { get; set; }
    }
}
