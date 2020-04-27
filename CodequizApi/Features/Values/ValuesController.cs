using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Classes;

namespace CodequizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                QuestionsRepository questionsRepository = new QuestionsRepository();
                IEnumerable<Question> questions = await questionsRepository.GetAllQuestions();
                return new JsonResult(questions);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
    }
}
