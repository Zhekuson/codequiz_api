using Domain.Models;
using Domain.Models.Questions;
using Domain.Models.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<IEnumerable<Question>> GetQuestionsByTag(Tag tag);
        Task<Question> GetQuestionByID(int ID);
        Task InsertQuestion(Question question);
    }
}
