using Domain.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces.Stats
{
    public interface IStatsService
    {
        public Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByUserEmail(string email);

        public Task InsertQuizAttempt(QuizAttempt quizAttempt);
    }
}
