﻿using Domain.Models;
using Domain.Models.Questions;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IQuestionService
    {
        Task<Question> GetQuestionByID(int id);
        Task<IEnumerable<Question>> GetAllQuestions();
        Task InsertQuestion(Question question);
    }
}
