using System;

namespace crypto.bot.backend.Services.Token
{
    public interface ITokenService
    {
        void Add(Guid tokenId, string jwt);

        string Get(Guid tokenId);
    }
}