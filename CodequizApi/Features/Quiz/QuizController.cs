using Domain.Models.Questions;
using Domain.Models.Quiz;
using Domain.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Exceptions;
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
    public class QuizController : Controller
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
            try
            {
                await quizService.InsertQuiz(quiz);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("custom")]
        public async Task<IActionResult> GetCustomQuiz([FromQuery] int questionsCount, [FromQuery] int minutesCount, [FromBody] IEnumerable<Tag> tags)
        {
            try
            {
                return new JsonResult(await quizService.GetCustomQuiz(tags, questionsCount, minutesCount));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("exam")]
        public async Task<IActionResult> GetExamQuiz()
        {
            try
            {
                return new JsonResult(await quizService.GetExamQuiz());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetAllRandomQuiz()
        {
            try
            {
                return new JsonResult(await quizService.GetAllRandomQuiz());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("answer")]
        public async Task<IActionResult> WriteResult([FromBody] QuizAttempt quizAttempt, [FromQuery] string email)
        {
            try
            {
                await statsService.InsertQuizAttempt(quizAttempt, email);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


    }

}
