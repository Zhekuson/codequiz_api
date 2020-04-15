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
        public async Task<IActionResult> Token(string email)
        {
            try
            {
                var identity = await GetIdentity(email);

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
                    accessToken = encodedJwt
                };

                return Json(response);
            }//TODO add catch exceptions
            catch (Exception)
            {
                return (StatusCode(500));
            }
        }
        private async Task<ClaimsIdentity> GetIdentity(string email)
        {
            User person = await userService.GetUserByEmail(email);
            if (person == null)
            {
                // если пользователя не найдено
                person = new User();
                person.Email = email;
                await userService.AddUser(person);
            }
            var claims = new List<Claim>
            {
                    new Claim("email",email)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            return claimsIdentity;
        }

    }
}
