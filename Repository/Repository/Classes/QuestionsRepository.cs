using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Classes
{
    public class QuestionsRepository:IQuestionsRepository
    {
        public IEnumerable<Question> loadAllQuestions()
        {
            return null;
        }
        public IEnumerable<Question> getQuestionsById(int id)
        {
            return null;
        }
    }
}
