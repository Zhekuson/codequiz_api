using Domain.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces.QuizAttempts
{
    public interface IQuizAttemptRepository
    {
        public Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByEmail(string email);

        public Task InsertQuizAttempt(QuizAttempt quizAttempt);
    }
}
