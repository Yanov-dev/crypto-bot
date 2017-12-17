using System.Collections.Generic;
using crypto.bot.backend.Models.CryptoTrigger;

namespace crypto.bot.backend.Repositories.Trigger
{
    public interface ITriggerRepository
    {
        void AddTrigger<T>(T trigger, long telegramUserId) where T : CryptoTrigger;

        IEnumerable<T> GetUserTriggers<T>(long telegramUserId) where T : CryptoTrigger;
    }
}