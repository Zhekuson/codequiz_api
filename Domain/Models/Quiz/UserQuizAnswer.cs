using Domain.Models.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Quiz
{
    class UserQuizAnswer
    {

        Quiz Quiz { get; set; }
        IEnumerable<Answer> UserAnswers { get; set; }

    }
}
