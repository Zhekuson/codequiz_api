using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Services.Interfaces;

namespace CodequizApi.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        readonly IUserService userService;
        readonly IMailService mailService;
        public UsersController(IUserService userService, IMailService mailService)
        {
            this.userService = userService;
            this.mailService = mailService;
        }
        [HttpPut("/{email}")]
        public async Task<IActionResult> SendVerificationEmail(string email)
        {
            try
            {
                int code = await mailService.SendVerificationEmail(email);
                return new JsonResult(code);
            }
            catch (Exception e)
            {
                //if error happened
                return StatusCode(500);
            }
            
        }
    }
}