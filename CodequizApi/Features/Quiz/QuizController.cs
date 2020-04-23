using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Services.Interfaces;
using Services.Services.Interfaces.Stats;
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
        readonly IStatsService statsService;
        readonly IUserService userService;
        public QuizController(IQuizService quizService, IStatsService statsService, IUserService userService)
        {
            this.quizService = quizService;
            this.statsService = statsService;
            this.userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            return new JsonResult(await quizService.GetQuizById(id));
        }

        [HttpPost("/custom")]
        public async Task<IActionResult> GetCustomQuiz([FromBody]IEnumerable<Tag> tags)
           // [FromBody] int minutesCount, [FromBody] int questionCount)
        {
            int questionsCount = 0;
            int minutesCount = 0;
            return new JsonResult(await quizService.GetCustomQuiz(tags, questionsCount, minutesCount));       
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
        public async Task<IActionResult> WriteResult ([FromBody] QuizAttempt quizAttempt)
        {
            await statsService.InsertQuizAttempt(quizAttempt);
            return Ok();
        }


    }

}
