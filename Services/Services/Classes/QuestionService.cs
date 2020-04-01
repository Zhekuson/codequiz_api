using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.Classes
{
    public class QuestionService : IQuestionService
    {
        public readonly IQuestionsRepository repository;
        public QuestionService(IQuestionsRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            return repository.GetAllQuestions();
        }

        public Question GetQuestionByID(int id)
        {
            return repository.GetQuestionByID(id);
        }
    }
}
