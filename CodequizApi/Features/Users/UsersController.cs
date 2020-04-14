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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            User user = userService.GetUserById(id);
            JsonResult result = new JsonResult(user);
            return result;
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            User user = userService.GetUserByEmail(email);
            return new JsonResult(user);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var body = Request.Body;
            return Ok();
        } 

    }
}