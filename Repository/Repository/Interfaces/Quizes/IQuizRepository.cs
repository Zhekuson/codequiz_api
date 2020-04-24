using Domain.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces.Quizes
{
    public interface IQuizRepository
    {
        public Task<Quiz> GetQuizById(int id);

        public Task<int> InsertQuiz(Quiz quiz);
    }
}
