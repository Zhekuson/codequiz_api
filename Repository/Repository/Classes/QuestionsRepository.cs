using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository.Classes
{
    public class QuestionsRepository:IQuestionsRepository
    {
        List<Question> allQuestions;
        public QuestionsRepository()
        {
            allQuestions = LoadAllQuestions() as List<Question>;
        }
        public static IEnumerable<Question> LoadAllQuestions()
        {
            //TODO here will be database call
            IEnumerable<Question> questions = new List<Question>();
            for (int i = 0; i < 10000; i++)
            {
                (questions as List<Question>).Add(new Question(i,$"text {i}"));
            }
            return questions;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return allQuestions;
        }

        public Question GetQuestionByID(int ID)
        {
            return allQuestions.Find((x) => x.ID == ID);
        }

        public IEnumerable<Question> GetQuestionsByTag(string tag)
        {
            return allQuestions.FindAll(x => x.Tags.Contains(tag));
        }
    }
}
