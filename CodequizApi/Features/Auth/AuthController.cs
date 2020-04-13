using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodequizApi.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        [HttpPost("token")]
        public IActionResult GetToken()
        {

        }
    }
}
