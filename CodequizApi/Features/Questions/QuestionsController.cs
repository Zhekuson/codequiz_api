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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Question> questions = await questionService.GetAllQuestions() as List<Question>;
            return new JsonResult(questions);
        }


    }
}
