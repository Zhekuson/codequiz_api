

using Domain.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.Questions
{ 

    public class Question
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Answer> RightAnswer { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

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
