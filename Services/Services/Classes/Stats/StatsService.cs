using Domain.Models.Quiz;
using Repository.Repository.Interfaces.QuizAttempts;
using Services.Services.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes.Stats
{
    class StatsService : IStatsService
    {
        readonly IQuizAttemptRepository quizAttemptRepository;
        public StatsService(IQuizAttemptRepository quizAttemptRepository)
        {
            this.quizAttemptRepository = quizAttemptRepository;
        }
        public Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByUserEmail(string email)
        {
            return quizAttemptRepository.GetQuizAttemptsByEmail(email);
        }
        public async Task InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            await quizAttemptRepository.InsertQuizAttempt(quizAttempt);
        }
    }
}
