using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Classes
{
    public class QuestionsRepository:IQuestionsRepository
    {
        List<Question> allQuestions = LoadAllQuestions() as List<Question>;
        public static IEnumerable<Question> LoadAllQuestions()
        {
            return null;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return allQuestions;
        }

        public Question GetQuestionByID(int ID)
        {
            return allQuestions.Find((x) => x.ID == ID);
        }
    }
}
