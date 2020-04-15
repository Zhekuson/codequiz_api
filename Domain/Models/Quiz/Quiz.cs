﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models.Questions;
namespace Domain.Models.Quiz
{
    class Quiz
    {
        int ID { get; set; }

        int UserId { get; set; }

        QuizType QuizType { get; set; }

        IEnumerable<Question> Questions { get; set; }

        DateTime StartDateTime { get; set; }

        DateTime EndDateTime { get; set; }

    }
}