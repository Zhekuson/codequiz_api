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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            User user = new User();
            JsonResult result = new JsonResult(user);
            return result;
        }
        
        
        //public Task<IActionResult> GetUserTagStats(string JWT)
        //{

        //}
    }
}