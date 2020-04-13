using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CodequizApi.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        //public Task<IActionResult> GetJWT()
        //{

        //}

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            User user = userService.GetUserById(id);
            JsonResult result = new JsonResult(user);
            return result;
        }
        //[HttpGet("/stats/{id}")]
        //public async Task<IActionResult> GetUserStats()
        //{

        //}
        
        //public Task<IActionResult> GetUserTagStats(string JWT)
        //{

        //}
    }
}