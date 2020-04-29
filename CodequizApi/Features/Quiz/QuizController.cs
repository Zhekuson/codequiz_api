using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Exceptions
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
        [HttpPost("insert")]
        public async Task<IActionResult> InsertQuiz([FromBody] Domain.Models.Quiz.Quiz quiz)
        {
            await quizService.InsertQuiz(quiz);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            try
            {
                return new JsonResult(await quizService.GetQuizById(id));
            }
            catch (QuizNotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("custom")]
        public async Task<IActionResult> GetCustomQuiz([FromQuery] int questionsCount, [FromQuery] int minutesCount,[FromBody]IEnumerable<Tag> tags)
        {
            return new JsonResult(await quizService.GetCustomQuiz(tags, questionsCount, minutesCount));       
        }

        [HttpGet("exam")]
        public async Task<IActionResult> GetExamQuiz()
        {
            return new JsonResult(await quizService.GetExamQuiz());
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetAllRandomQuiz()
        {
            return new JsonResult(await quizService.GetAllRandomQuiz());
        }

        [HttpPost("answer")]
        public async Task<IActionResult> WriteResult ([FromBody] QuizAttempt quizAttempt, [FromQuery] string email)
        {
            try { 
                await statsService.InsertQuizAttempt(quizAttempt, email);
                return Ok();
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }


    }

}
