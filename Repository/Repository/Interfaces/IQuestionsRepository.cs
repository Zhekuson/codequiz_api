using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Interfaces
{
    public interface IQuestionsRepository
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestionByID(int ID);
    }
}
