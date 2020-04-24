using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Quiz;
using Domain.Models.Tags;

namespace Services.Services.Interfaces
{
    public interface IQuizService
    {
        public Task<Quiz> GetAllRandomQuiz();
        public Task<Quiz> GetExamQuiz();
        public Task<Quiz> GetCustomQuiz(IEnumerable<Tag> tags, int questionsCount, int minutes);
        public Task<Quiz> GetQuizById(int id);
        public Task<int> InsertQuiz(Quiz quiz);
    }
}
