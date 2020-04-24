using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Users;
using Repository.Repository.Exceptions;
using Repository.Repository.Interfaces;
using Repository.Repository.Interfaces.QuizAttempts;
using Repository.Repository.Interfaces.Quizes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.QuizAttempts
{
    class QuizAttemptRepository : EntityRepository, IQuizAttemptRepository
    {
        readonly IUsersRepository usersRepository;
        readonly IQuizRepository quizRepository;

        public QuizAttemptRepository(IUsersRepository usersRepository, IQuizRepository quizRepository)
        {
            this.usersRepository = usersRepository;
            this.quizRepository = quizRepository;
        }
        public async Task<IEnumerable<QuizAttempt>> GetQuizAttemptsByEmail(string email)
        {
            return await ExecuteQueryQuizAttemptsByEmail(email);
        }

        public async Task InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            await ExecuteQueryInsertQuizAttemptAndUserAnswers(quizAttempt);
        }

        [QueryExecutor]
        private async Task<UserQuizAnswer> ExecuteQueryGetUserAnswerByAttempt(QuizAttempt quizAttempt)
        {
            UserQuizAnswer userQuizAnswer = new UserQuizAnswer();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM [dbo].[UserAnswer] " +
                    $"JOIN [dbo].[Answer] ON Answer.id = UserAnswer.answer_id WHERE" +
                    $"  [UserAnswer].quiz_attempt_id = {quizAttempt.Id}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                       
                        while (reader.Read())
                        {
                            Answer answer = new Answer();
                            answer.AnswerText = reader.GetStringByName("answer_text");
                            answer.Id = reader.GetInt32ByName("answer_id");
                            answer.IsRight = reader.GetBoolByName("is_right");
                            answer.QuestionId = reader.GetInt32ByName("question_id");
                            userQuizAnswer.UserAnswers = userQuizAnswer.UserAnswers.Append(answer);
                        }
                    }
                }
            }
            return userQuizAnswer;
        }

        [QueryExecutor]
        private async Task ExecuteQueryInsertQuizAttemptAndUserAnswers(QuizAttempt quizAttempt)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (connection.BeginTransaction())
                {
                    //insert quiz attempt
                    SqlCommand command = CreateCommand($"INSERT INTO [dbo].[QuizAttempt](" +
                        $"quiz_id, user_id, start_time, end_time" +
                        $") VALUES ({quizAttempt.Quiz.ID}, {quizAttempt.UserId}," +
                        $"{quizAttempt.StartDateTime},{quizAttempt.EndDateTime})", connection);
                        command.ExecuteNonQuery();
                    //insert user answers
                    foreach(var answer in quizAttempt.UserQuizAnswer.UserAnswers) 
                    {
                        command = CreateCommand($"INSERT INTO [dbo].[UserAnswer](quiz_attempt_id," +
                         $"question_id, answer_id) VALUES ({quizAttempt.Id}, {answer.QuestionId}, {answer.Id})", connection);
                        command.ExecuteNonQuery();
                    }

                }

            }
        }
        [QueryExecutor]
        private async Task<IEnumerable<QuizAttempt>> ExecuteQueryQuizAttemptsByEmail(string email)
        {
            User user = await usersRepository.GetUserByEmail(email);
            IEnumerable<QuizAttempt> quizAttempts = new List<QuizAttempt>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT qa.id, qa.quiz_id," +
                    $" qa.start_time, qa.end_time FROM [dbo].[QuizAttempt] qa WHERE qa.[user_id] = {user.ID}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {  
                        while (reader.Read())
                        {
                            QuizAttempt quizAttempt = new QuizAttempt();
                            quizAttempt.Id = reader.GetInt32ByName("id");
                            quizAttempt.StartDateTime = reader.GetDateTimeByName("start_time");
                            quizAttempt.EndDateTime = reader.GetDateTimeByName("end_time");
                            quizAttempt.Quiz = new Quiz();
                            quizAttempt.Quiz.ID = reader.GetInt32ByName("quiz_id");
                            quizAttempts = quizAttempts.Append(quizAttempt);
                        }
                    }
                    else
                    {
                        throw new QuizAttemptsNotFound();
                    }
                }
                foreach(var quizAttempt in quizAttempts)
                {
                    quizAttempt.Quiz = await quizRepository.GetQuizById(quizAttempt.Quiz.ID);
                    quizAttempt.UserQuizAnswer = await ExecuteQueryGetUserAnswerByAttempt(quizAttempt);
                }
            }
            return quizAttempts;
        }

    }
}
