using Domain.Models.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Quiz
{
    /// <summary>
    /// Class with collection of collection of answers
    /// </summary>
    public class UserQuizAnswer
    {
        public IEnumerable<IEnumerable<Answer>> UserAnswers { get; set; }
    }
}
