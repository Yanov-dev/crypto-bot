using System;
using crypto.bot.backend.dto;
using crypto.bot.backend.Services;
using crypto.bot.backend.Services.Token;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        [HttpPost("login")]
        public LoginResponse Login(
            [FromBody] LoginRequest loginRequest,
            [FromServices] ITokenService tokenService)
        {
            var response = new LoginResponse();
            
            var jwt = tokenService.Get(loginRequest.TokenId);
            if (jwt == null)
            {
                return new LoginResponse
                {
                    Error = "token not found"
                };  
            }

            return new LoginResponse
            {
                Jwt = jwt
            };
        }
    }
}