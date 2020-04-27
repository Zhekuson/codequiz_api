using Domain.Models.Questions;
using Domain.Models.Quiz;
using Repository.Repository.Exceptions;
using Repository.Repository.Interfaces;
using Repository.Repository.Interfaces.Quizes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.Quizes
{
    public class QuizRepository : EntityRepository, IQuizRepository
    {
        readonly IQuestionsRepository questionsRepository;
        public QuizRepository(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }
        public async Task<Quiz> GetQuizById(int id)
        {
            return await ExecuteQueryGetQuizById(id);
        }

        public async Task<int> InsertQuiz(Quiz quiz)
        {
           return await ExecuteQueryInsertQuiz(quiz);
        }

        [QueryExecutor]
        private async Task<Quiz> ExecuteQueryGetQuizById(int id)
        {
            Quiz quiz = new Quiz();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM Quiz JOIN QuizQuestion ON  " +
                    $"Quiz.id = QuizQuestion.quiz_id  WHERE Quiz.id ={id}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int qid = reader.GetInt32ByName("question_id");
                        quiz.QuizType = (QuizType)reader.GetByteByName("quiz_type_id");
                        quiz.ID = reader.GetInt32ByName("id");
                        quiz.Minutes = reader.GetInt32ByName("minutes");
                        quiz.Questions = new List<Question>();
                        Question question = await questionsRepository.GetQuestionByID(qid);
                        quiz.Questions = quiz.Questions.Append(question);
                        while (reader.Read()) 
                        {
                            qid = reader.GetInt32ByName("question_id");
                            question = await questionsRepository.GetQuestionByID(qid);
                            quiz.Questions = quiz.Questions.Append(question);
                        }
                    }
                    else
                    {
                        throw new QuizNotFoundException();
                    }
                }
            }
            return quiz;
        }
        [QueryExecutor]
        private async Task<int> ExecuteQueryInsertQuiz(Quiz quiz)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"INSERT INTO Quiz " +
                    $" (quiz_type_id, minutes)" +
                    $" VALUES ({(int)quiz.QuizType}, {quiz.Minutes}) SELECT @@IDENTITY AS ID", connection);

                using (SqlDataReader sqlDataReader = command.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        sqlDataReader.Read();
                        quiz.ID = (int)sqlDataReader.GetDecimalByName("ID");
                    }
                }
                foreach (var question in quiz.Questions)
                {
                    command = CreateCommand($"INSERT INTO QuizQuestion (quiz_id, question_id) " +
                        $"VALUES ({quiz.ID}, {question.ID})", connection);
                    command.ExecuteNonQuery();
                }

            }
            return quiz.ID;
        }

    }
}
