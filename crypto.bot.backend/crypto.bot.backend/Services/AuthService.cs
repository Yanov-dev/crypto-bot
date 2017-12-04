using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using crypto.bot.backend.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace crypto.bot.backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthOptions _authOptions;

        public AuthService(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        private ClaimsIdentity GetIdentity(long telegramUserId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, telegramUserId.ToString()),
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public string GenerateJwt(long telegramUserId)
        {
            var identity = GetIdentity(telegramUserId);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Host,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}