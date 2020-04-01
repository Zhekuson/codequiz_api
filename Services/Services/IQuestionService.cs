using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IQuestionService
    {
        Question GetQuestionByID(int id);
        IEnumerable<Question> GetAllQuestions();
    }
}
