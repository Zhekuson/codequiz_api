using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodequizApi.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {

        readonly IUserService userService;
        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("token")]
        public IActionResult Token(string email)
        {
            var identity = GetIdentity(email);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt
            };

            return Json(response);
        }
        private ClaimsIdentity GetIdentity(string email)
        {
            User person = userService.GetUserByEmail(email);
            if (person == null)
            {
                person = new Domain.Models.User();
                person.Email = email;
                userService.AddUser(person);
            }

            // если пользователя не найдено
     
            var claims = new List<Claim>
                {
                    new Claim("email",email)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            return claimsIdentity;
        }

    }
}
