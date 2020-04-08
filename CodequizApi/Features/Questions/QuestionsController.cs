using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Classes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodequizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService; 
        }
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return id;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            /*Question question = new Question(3,"dvasc");
            List<Question> questions = new List<Question>();
            questions.Add(question);*/
            List<Question> questions = questionService.GetAllQuestions() as List<Question>;
            JsonResult jsonResult = new JsonResult(questions);
            return  jsonResult;
        }

    }
}
