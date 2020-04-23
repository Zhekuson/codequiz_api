using Domain.Models;
using Domain.Models.Questions;
using Domain.Models.Tags;
using Microsoft.SqlServer.Server;
using Repository.Repository.DatabaseConnection;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes
{
    public class QuestionsRepository : EntityRepository, IQuestionsRepository
    {
        
        public QuestionsRepository()
        {

        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await ExecuteQueryGetAllQuestions();
        }


        public async Task<Question> GetQuestionByID(int ID)
        {
            return await ExecuteQueryGetQuestionById(ID);
        }


        public async Task<IEnumerable<Question>> GetQuestionsByTag(Tag tag)
        {
            return await ExecuteQueryGetQuestionsByTag(tag);
        }

        public async Task InsertQuestion(Question question)
        {
            await ExecuteQueryInsertQuestion(question);
        }
        


        [QueryExecutor]
        private async Task<Question> ExecuteQueryGetQuestionById(int id)
        { 
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM dbo.Question WHERE Question.id = {id} ", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Question question = new Question();
                        question.ID = reader.GetInt32(reader.GetOrdinal("id"));
                        question.QuestionText = reader.GetString(reader.GetOrdinal("question_text"));
                        question.Type = (QuestionType)reader.GetInt32(reader.GetOrdinal("question_type_id"));
                    }
                }

            }

            throw new NotImplementedException();
         
        }
        [QueryExecutor]
        private async Task<IEnumerable<Question>> ExecuteQueryGetQuestionsByTag(Tag tag)
        {
            IEnumerable<Question> questions = new List<Question>(); 
            using (SqlConnection connection = GetConnection())
            { 
                connection.Open();
                SqlCommand command = CreateCommand("SELECT  q.id, q.question_type_id, qty.type_name,q.question_text" +
                "FROM[dbo].[Tag] t + JOIN[dbo].[QuestionTag] qt ON qt.tag_id = t.id"+
                "JOIN[dbo].[Question] q ON q.id = qt.question_id "+
                "JOIN[dbo].[QuestionType] qty ON qty.id = q.question_type_id"+
                $"WHERE t.tag_name = {tag.Name}",connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Question question = new Question();
                        question.ID = reader.GetInt32ByName("id");
                        question.QuestionText = reader.GetStringByName("question_text");
                        question.Type = (QuestionType)reader.GetInt32ByName("question_type_id");
                    }
                }
                foreach (var question in questions)
                {
                    command = CreateCommand("SELECT Tag.id, Tag.tag_name FROM Tag JOIN QuestionTag" +
                    "ON QuestionTag.tag_id = Tag.id JOIN Question ON Question.id = QuestionTag.question_id"+
                    $" WHERE Question.id = {question.ID}", connection);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            question.Tags = new List<Tag>();
                            while (reader.HasRows)
                            {
                                Tag tag1 = new Tag();
                                tag1.ID = reader.GetInt32ByName("id");
                                tag1.Name = reader.GetStringByName("tag_name");
                                question.Tags.Append(tag1);
                            }
                        }
                    }
                    command = CreateCommand("SELECT Answer.id, Answer.answer_text, " +
                        "Answer.is_right, Answer.question_id FROM Answer JOIN Question"+
                        $"ON Question.id = Answer.question_id WHERE Question.id = {question.ID}", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            question.Answers = new List<Answer>();
                            while (reader.HasRows)
                            {
                                Answer answer = new Answer();
                                answer.Id = reader.GetInt32ByName("id");
                                answer.AnswerText = reader.GetStringByName("answer_text");
                                answer.IsRight = reader.GetBoolByName("is_right");
                                answer.QuestionId = question.ID;
                                if (answer.IsRight)
                                {
                                    question.RightAnswer.Append(answer);
                                }
                                question.Answers.Append(answer);
                            }
                        }
                    }
                }
            }

            return questions;
        }
        [QueryExecutor]
        private async Task ExecuteQueryInsertQuestion(Question question)
        {
            using (SqlConnection connection =  GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand("INSERT INTO [dbo].Question (id, question_text, question_type_id)", connection);

                command.ExecuteNonQuery();
            }
            throw new NotImplementedException();
        }
        [QueryExecutor]
        private async Task<IEnumerable<Question>> ExecuteQueryGetAllQuestions()
        {
            IEnumerable<Question> questions = new List<Question>();
            using (SqlConnection connection =  GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand("SELECT * FROM Question", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Question question = new Question();
                            question.ID = reader.GetInt32(reader.GetOrdinal("id"));
                            question = await ExecuteQueryGetQuestionById(question.ID);
                            questions.Append(question);
                        }
                    }
                }
            }
            return questions;
        }

    }
}