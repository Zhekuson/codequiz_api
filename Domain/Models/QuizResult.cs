using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int TotalQuestions { get; set; }
        public int RightAnswers { get; set; }
        public DateTime Date { get; set; }
    }
}
