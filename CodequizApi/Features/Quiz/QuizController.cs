using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
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
        readonly IQuizService quizService;
        QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            return new JsonResult(quizService.GetQuizById(id));
        }

        [HttpPost("/custom")]
        public async Task<IActionResult> GetCustomQuiz([FromBody]IEnumerable<Tag> tags,
            [FromBody] int minutesCount, [FromBody] int questionCount)
        {
            return new JsonResult(await quizService.GetCustomQuiz(tags, questionCount, minutesCount));       
        }

        [HttpPost("/exam")]
        public async Task<IActionResult> GetExamQuiz()
        {
            return new JsonResult(await quizService.GetExamQuiz());
        }
        [HttpPost("/random")]
        public async Task<IActionResult> GetAllRandomQuiz()
        {
            return new JsonResult(await quizService.GetAllRandomQuiz());
        }
        
        [HttpPut("/answer")]
        public async Task<IActionResult> GetUserAnswers([FromBody] IEnumerable<IEnumerable<Answer>> answers)
        {
            UserQuizAnswer userQuizAnswer = new UserQuizAnswer();
            userQuizAnswer.UserAnswers = answers;
            return new JsonResult(await quizService.MakeReport(userQuizAnswer));
        }


    }

}
