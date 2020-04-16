using Domain.Models.Quiz;
using Repository.Repository.Interfaces.QuizAttempts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.QuizAttempts
{
    class QuizAttemptRepository : EntityRepository, IQuizAttemptRepository
    {
        public async Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            throw new NotImplementedException();
        }
    }
}
