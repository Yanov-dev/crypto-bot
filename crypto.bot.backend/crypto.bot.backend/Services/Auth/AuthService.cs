using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using crypto.bot.backend.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace crypto.bot.backend.Services.Auth
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public string GenerateJwt(long telegramUserId)
        {
            var identity = GetIdentity(telegramUserId);

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}