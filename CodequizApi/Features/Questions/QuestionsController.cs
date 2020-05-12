using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Questions;


namespace CodequizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : Controller
    {
        readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Question> questions = await questionService.GetAllQuestions() as List<Question>;
                return new JsonResult(questions);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertQuestion([FromBody] Question question)
        {
            try
            {
                await questionService.InsertQuestion(question);
                return Ok();
            }
            catch(Exception e)
            {
                return new JsonResult(StatusCode(500));
            }
        }


    }
}
