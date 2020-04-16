using Domain.Models.Quiz;
using Repository.Repository.Interfaces.Quizes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.Quizes
{
    public class QuizRepository : EntityRepository, IQuizRepository
    {
        public async Task<Quiz> GetQuizById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }
    }
}
