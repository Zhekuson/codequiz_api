using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum QuestionType
    {
        OPEN,
        SINGLE_CHOICE,
        MULTIPLE_CHOICE
    }
    public class Question
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string RightAnswer { get; set; }
        public IEnumerable<string> Answers { get; set; }

        public Question()
        {
                
        }
        public Question(int id, string text)
        {
            ID = id;
            QuestionText = text;
        }
    }
}
