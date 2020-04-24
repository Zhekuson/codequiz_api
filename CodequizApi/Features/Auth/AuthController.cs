using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Services.Interfaces;
using Services.Services.Exceptions;
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
    public class AuthController : Controller
    {

        readonly IUserService userService;
        readonly IMailService mailService;
        public AuthController(IUserService userService, IMailService mailService)
        {
            this.userService = userService;
            this.mailService = mailService;
        }
        [HttpPost("verification")]
        public async Task<IActionResult> SendVerificationEmail([FromQuery]string email)
        {
            int code = await mailService.SendVerificationEmail(email);
            int sessionId = await userService.PutAuthorizationCode(code);
            return new JsonResult(sessionId);
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery]int code,
            [FromQuery] int sessionId,[FromQuery] string email)
        {
            if (await userService.CheckCode(code, sessionId))
            {
                User user = new User();
                user.Email = email;
                await userService.AddUser(user);
                return await Token(email);
            }
            return StatusCode(401);
        }

        private async Task<IActionResult> Token(string email)
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
            }
            catch (UserUnauthorizedException)
            {
                return StatusCode(401);
            }
            catch (Exception)
            {
                return (StatusCode(500));
            }
        }
        private async Task<ClaimsIdentity> GetIdentity(string email)
        {
            User person = await userService.GetUserByEmail(email);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                       new Claim("email",email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                return claimsIdentity;
            }
            throw new UserUnauthorizedException();
        }

    }
}
