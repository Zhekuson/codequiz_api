using Domain.Models;
using Domain.Models.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestionByID(int ID);
    }
}
