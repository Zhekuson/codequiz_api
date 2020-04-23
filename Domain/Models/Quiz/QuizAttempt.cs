using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Quiz
{
    /// <summary>
    /// Quiz attempt
    /// </summary>
    public class QuizAttempt
    {
        public int Id { get; set; }
        public Quiz Quiz { get; set; }
        public int UserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public UserQuizAnswer UserQuizAnswer { get; set; }

    }
}
