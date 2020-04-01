using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IQuestionService
    {
        //IQuestionsRepository repository { get; set; }
        Question GetQuestionByID(int id);
        IEnumerable<Question> GetAllQuestions();
    }
}
