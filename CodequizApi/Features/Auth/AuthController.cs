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
        [HttpPost("/token")]
        public IActionResult Token(string email)
        {
            var identity = GetIdentity(email);
            if (identity == null)
            {
                return new BadRequestResult();
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
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);

        }
        private ClaimsIdentity GetIdentity(string email)
        {
            User person = userService.GetUserByEmail(email);
            if (person != null)
            {
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                //    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                //};
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else
            {
                //TODO add insert 
            }

            // если пользователя не найдено
            return null;
        }
    }
}
