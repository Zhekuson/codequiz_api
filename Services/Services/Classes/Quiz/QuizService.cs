using Domain.Models.Quiz;
using Domain.Models.Tags;
using Repository.Repository.Classes;
using Repository.Repository.Interfaces;
using Repository.Repository.Interfaces.Quizes;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class QuizService : IQuizService
    {
        readonly IQuestionsRepository questionsRepository;
        readonly IQuizRepository quizRepository;


        QuizService(IQuestionsRepository questionsRepository, IQuizRepository quizRepository)
        {
            this.questionsRepository = questionsRepository;
            this.quizRepository = quizRepository;
        }

        #region quiz generators
        public async Task<Quiz> GetAllRandomQuiz()
        {
            throw new NotImplementedException();
        }

        public async Task<Quiz> GetCustomQuiz(IEnumerable<Tag> tags, int questionsCount, int minutes)
        {
            throw new NotImplementedException();
        }

        public async Task<Quiz> GetExamQuiz()
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<Quiz> GetQuizById(int id)
        {
            return await quizRepository.GetQuizById(id);
        }

        public async Task InsertQuiz(Quiz quiz)
        {
            await quizRepository.InsertQuiz(quiz);
        }
    }
}
