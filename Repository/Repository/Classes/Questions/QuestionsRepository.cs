using Domain.Models;
using Domain.Models.Questions;
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
    public class QuestionsRepository : IQuestionsRepository
    {
        
        public QuestionsRepository()
        {

        }
        public async static Task<IEnumerable<Question>> LoadAllQuestions()
        {

        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Question"))
                    {
                        var reader = command.ExecuteReader();

                    }

                }
                catch (SqlException e)
                {

                }
            }

           // return allQuestions;
        }


        public async Task<Question> GetQuestionByID(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTag(string tag)
        {
            throw new NotImplementedException();
        }

       
    }
}
    //IEnumerable<Question> questions = new List<Question>();
    //        for (int i = 0; i< 10000; i++)
    //        {
    //            (questions as List<Question>).Add(new Question(i,$"text {i}"));
    //        }
    //        return questions;