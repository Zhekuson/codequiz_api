using Domain.Models;
using Domain.Models.Questions;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class QuestionService : IQuestionService
    {
        public readonly IQuestionsRepository repository;
        public QuestionService(IQuestionsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await repository.GetAllQuestions();
        }

        public Task<Question> GetQuestionByID(int id)
        {
            return repository.GetQuestionByID(id);
        }
    }
}
