using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Tags;
using MimeKit.Encodings;
using Repository.Repository.Classes;
using Repository.Repository.Interfaces;
using Repository.Repository.Interfaces.Quizes;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class QuizService : IQuizService
    {
        const int examQuestionsCount = 40;
        const int allRandomQuestionsCount = 10;
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
            Quiz quiz = new Quiz();
            quiz.QuizType = QuizType.AllRandom;
            List<Question> questions = await questionsRepository.GetAllQuestions() as List<Question>;
            Random random = new Random();
            for (int i = 0; i < Math.Min(allRandomQuestionsCount, questions.Count); i++)
            {
                int index = random.Next(0, questions.Count);
                quiz.Questions = quiz.Questions.Append(questions[index]);
                questions.RemoveAt(index);
            }
            quiz.ID = await InsertQuiz(quiz);
            return quiz;
        }

        public async Task<Quiz> GetCustomQuiz(IEnumerable<Tag> tags, int questionsCount, int minutes)
        {
            HashSet<Question> allQuestions = new HashSet<Question>();
            foreach (Tag tag in tags)
            {
               IEnumerable<Question> questions = await questionsRepository.GetQuestionsByTag(tag);
                foreach (Question question in questions)
                {
                    allQuestions.Add(question);
                }
            }
            Quiz quiz = new Quiz();
            quiz.QuizType = QuizType.Custom;
            quiz.Questions = allQuestions;
            quiz.ID = await InsertQuiz(quiz);
            return quiz;
        }

        public async Task<Quiz> GetExamQuiz()
        {
            Quiz quiz = new Quiz();
            quiz.QuizType = QuizType.Exam;
            List<Question> questions = await questionsRepository.GetAllQuestions() as List<Question>;
            Random random = new Random();
            for (int i = 0; i < Math.Min(examQuestionsCount, questions.Count); i++)
            {
                int index = random.Next(0, questions.Count);
                quiz.Questions = quiz.Questions.Append(questions[index]);
                questions.RemoveAt(index);
            }
            quiz.ID = await InsertQuiz(quiz);
            return quiz;
        }
        #endregion

        public async Task<Quiz> GetQuizById(int id)
        {
            return await quizRepository.GetQuizById(id);
        }

        public async Task<int> InsertQuiz(Quiz quiz)
        {
            return await quizRepository.InsertQuiz(quiz);
        }
    }
}
