using Domain.Models.Quiz;
using Domain.Models.Tags;
using Repository.Repository.Classes;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    class QuizService : IQuizService
    {
        readonly QuestionsRepository questionsRepository;

        QuizService(QuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public async Task<Quiz> GetAllRandomQuiz()
        {
            throw new NotImplementedException();
        }

        public Task<Quiz> GetCustomQuiz(IEnumerable<Tag> tags, int questionsCount, int minutes)
        {
            throw new NotImplementedException();
        }

        public async Task<Quiz> GetExamQuiz()
        {
            throw new NotImplementedException();
        }
    }
}
