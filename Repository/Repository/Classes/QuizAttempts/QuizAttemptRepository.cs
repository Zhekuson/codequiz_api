using Domain.Models.Quiz;
using Repository.Repository.Interfaces.QuizAttempts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.QuizAttempts
{
    class QuizAttemptRepository : IQuizAttemptRepository
    {
        public Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
