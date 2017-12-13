using System.Collections.Generic;
using crypto.bot.backend.Models;

namespace crypto.bot.backend.Repositories
{
    public interface ICryptoRepository
    {
        void UpdateCurrencies(CurrencyInfo[] infos);

        List<CurrencyInfo> GetCurrencies();

        void AddTrigger(CurrencyTrigger trigger);

        List<CurrencyTrigger> GetTriggers(long telegramUserId);
    }
}