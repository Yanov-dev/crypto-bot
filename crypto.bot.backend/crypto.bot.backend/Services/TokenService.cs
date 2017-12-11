using System;
using System.Collections.Generic;

namespace crypto.bot.backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly Dictionary<Guid, string> _tokens;

        public TokenService()
        {
            _tokens = new Dictionary<Guid, string>();
        }

        public void Add(Guid tokenId, string jwt)
        {
            _tokens[tokenId] = jwt;
        }

        public string Get(Guid tokenId)
        {
            if (_tokens.TryGetValue(tokenId, out var jwt))
            {
                _tokens.Remove(tokenId);
                return jwt;
            }

            return null;
        }
    }
}