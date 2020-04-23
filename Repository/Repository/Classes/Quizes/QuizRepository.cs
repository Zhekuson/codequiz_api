﻿using Domain.Models.Quiz;
using Repository.Repository.Interfaces.Quizes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.Quizes
{
    public class QuizRepository : EntityRepository, IQuizRepository
    {
        public async Task<Quiz> GetQuizById(int id)
        {
            return await ExecuteQueryGetQuizById(id);
        }

        public async Task InsertQuiz(Quiz quiz)
        {
            await ExecuteQueryInsertQuiz(quiz);
        }

        [QueryExecutor]
        private async Task<Quiz> ExecuteQueryGetQuizById(int id)
        {

        }
        [QueryExecutor]
        private async Task ExecuteQueryInsertQuiz(Quiz quiz)
        {

        }
    }
}
