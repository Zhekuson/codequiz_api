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


        public QuizService(IQuestionsRepository questionsRepository, IQuizRepository quizRepository)
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
            quiz.Questions = new List<Question>();
            for (int i = 0; i < Math.Min(allRandomQuestionsCount, questions.Count()); i++)
            {
                int index = random.Next(0, questions.Count());
                quiz.Questions = quiz.Questions.Append(questions[index]);
                questions.RemoveAt(index);
            }
            quiz.ID = await InsertQuiz(quiz);
            return quiz;
        }

        public async Task<Quiz> GetCustomQuiz(IEnumerable<Tag> tags, int questionsCount)
        {
            HashSet<Question> allQuestions = new HashSet<Question>();
            foreach (Tag tag in tags)
            {
                List<Question> questions = await questionsRepository.GetQuestionsByTag(tag) as List<Question>;
                for(int i = 0; i < questions.Count(); i++)
                {
                    if(allQuestions.Where(x => x.ID == questions.ElementAt(i).ID).Count() == 0)
                    {
                        allQuestions.Add(questions.ElementAt(i));
                    }
                }
            }
          
            Quiz quiz = new Quiz();
            quiz.QuizType = QuizType.Custom;
            quiz.Questions = new List<Question>();
            Random random = new Random();
            for (int i = 0; i < Math.Min(questionsCount,allQuestions.Count()); i++)
            {
                Question question = allQuestions.ElementAt(random.Next(0, allQuestions.Count()));
                allQuestions.Remove(question);
                (quiz.Questions as List<Question>).Add(question);
            }
            quiz.ID = await InsertQuiz(quiz);
            return quiz;
        }

        public async Task<Quiz> GetExamQuiz()
        {
            Quiz quiz = new Quiz();
            quiz.QuizType = QuizType.Exam;
            List<Question> questions = (await questionsRepository.GetAllQuestions())as List<Question>;
            quiz.Questions = new List<Question>();
            Random random = new Random();
            for (int i = 0; i < Math.Min(examQuestionsCount, questions.Count()); i++)
            {
                int index = random.Next(0, questions.Count());
                quiz.Questions = quiz.Questions.Append(questions.ElementAt(index));
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
