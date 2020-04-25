using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodequizApi.Features.Stats
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController:Controller
    {
        IStatsService statsService;
        public StatsController(IStatsService statsService)
        {
            this.statsService = statsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserStats([FromQuery]string email)
        {   
            return new JsonResult(await statsService.GetQuizAttemptsByUserEmail(email));
        }
    }
}
