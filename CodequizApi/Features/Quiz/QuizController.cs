using Domain.Models.Questions;
using Domain.Models.Quiz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodequizApi.Features.Quiz
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController:Controller
    {
        [HttpPost("/custom")]
        public async Task<IActionResult> GetCustomQuiz([FromBody])
        {
            //todo add quiz custom
            QuizType.Custom
        }

        [HttpPost("/exam")]
        public async Task<IActionResult> GetExamQuiz([FromBody] )
        {
            QuizType.Exam
        }
        [HttpPost("/random")]
        public async Task<IActionResult> GetAllRandomQuiz()
        {
            QuizType.AllRandom
        }
        
        [HttpPut("/answer")]
        public async Task<IActionResult> GetUserAnswers([FromBody] IEnumerable<Answer> answers)
        {
            
        }


    }

}
