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
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes
{
    public class QuestionsRepository : EntityRepository,IQuestionsRepository
    {
        
        public QuestionsRepository()
        {

        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            throw new NotImplementedException();
        }


        public async Task<Question> GetQuestionByID(int ID)
        {
            return await ExecuteQueryGetQuestionById(ID);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetQuestionsByTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task InsertQuestion(Question question)
        {
            throw new NotImplementedException();
        }
        


        [QueryExecutor]
        private async Task<Question> ExecuteQueryGetQuestionById(int id)
        { 
            using (SqlConnection connection = await GetConnection())
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
    }
}