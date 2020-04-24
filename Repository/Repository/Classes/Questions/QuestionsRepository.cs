using Domain.Models;
using Domain.Models.Questions;
using Domain.Models.Tags;
using Microsoft.SqlServer.Server;
using Repository.Repository.DatabaseConnection;
using Repository.Repository.Exceptions;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
            Question question = new Question();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM dbo.Question WHERE Question.id = {id} ", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            question.ID = reader.GetInt32ByName("id");
                            question.QuestionText = reader.GetStringByName("question_text");
                            question.Type = (QuestionType)reader.GetByte(reader.GetOrdinal("question_type_id"));
                        }
                    }
                    else
                    {
                        throw new QuestionNotFoundException();
                    }
                }
                command = CreateCommand($"SELECT tag_id, tag_name FROM Question " +
                $" JOIN QuestionTag ON Question.id = QuestionTag.question_id " +
                $" JOIN Tag ON Tag.id = QuestionTag.tag_id WHERE Question.id = {question.ID}",connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        question.Tags = new List<Tag>();
                        while (reader.Read())
                        {
                            Tag tag = new Tag();
                            tag.ID = reader.GetInt32ByName("tag_id");
                            tag.Name = reader.GetStringByName("tag_name");
                            question.Tags = question.Tags.Append(tag);
                        }
                    }
                }
                command = CreateCommand($"SELECT * FROM Answer WHERE Answer.question_id = {question.ID}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        question.Answers = new List<Answer>();
                        while (reader.Read())
                        {
                            Answer answer = new Answer();
                            answer.Id = reader.GetInt32ByName("id");
                            answer.AnswerText = reader.GetStringByName("answer_text");
                            answer.IsRight = reader.GetBoolByName("is_right");
                            answer.QuestionId = question.ID;
                            question.Answers = question.Answers.Append(answer);
                        }
                    }
                }
            }
            return question;
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
                        reader.Read();
                        Question question = new Question();
                        question.ID = reader.GetInt32ByName("id");
                        question.QuestionText = reader.GetStringByName("question_text");
                        question.Type = (QuestionType)reader.GetInt32ByName("question_type_id");
                        questions = questions.Append(question);
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
                                question.Tags = question.Tags.Append(tag1);
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
                                question.Answers = question.Answers.Append(answer);
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
                using (connection.BeginTransaction())
                {
                    SqlCommand command = CreateCommand("INSERT INTO [dbo].Question (question_text, question_type_id)" +
                        $" VALUES ({question.QuestionText}, {(int)question.Type}) SELECT @@IDENTITY AS ID", connection);
                    using (SqlDataReader sqldatareader = command.ExecuteReader())
                    {
                        question.ID = (int)sqldatareader.GetDecimalByName("ID");
                    }


                    foreach (Answer answer in question.Answers)
                    {
                        command = CreateCommand("INSERT INTO [dbo].Answer (answer_text, is_right, question_id)" +
                            $" VALUES({answer.AnswerText}, {answer.IsRight}, {question.ID})", connection);
                    }

                    foreach (Tag tag in question.Tags)
                    {
                        command = CreateCommand($"SELECT * FROM Tag WHERE Tag.tag_name = {tag.Name}", connection);
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            sqlDataReader.Read();
                            tag.ID = sqlDataReader.GetInt32ByName("id");
                        }

                        command = CreateCommand("INSERT INTO [dbo].QuestionTag (question_id, tag_id)" +
                             $" VALUES({question.ID}, {tag.ID})", connection);
                    }
                }
            }
        
        }
        [QueryExecutor]
        private async Task<IEnumerable<Question>> ExecuteQueryGetAllQuestions()
        {
            IEnumerable<Question> questions = new List<Question>();
            using (SqlConnection connection = GetConnection())
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
                            questions = questions.Append(question);
                        }
                    }
                }
            }
            return questions;
        }

    }
}