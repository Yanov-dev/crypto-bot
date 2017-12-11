using System;

namespace crypto.bot.backend.Services
{
    public interface ITokenService
    {
        void Add(Guid tokenId, string jwt);
        
        string Get(Guid tokenId);
    }
}