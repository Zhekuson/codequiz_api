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
            throw new NotImplementedException();
        }

        public Question GetQuestionByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
