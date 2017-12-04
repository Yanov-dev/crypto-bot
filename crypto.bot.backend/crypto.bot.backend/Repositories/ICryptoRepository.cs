using System.Collections.Generic;
using crypto.bot.backend.Models;

namespace crypto.bot.backend.Repositories
{
    public interface ICryptoRepository
    {
        void UpdateCurrencies(CryptoInfo[] infos);

        List<CryptoInfo> GetCurrencies();

        void AddTrigger(CurrencyTrigger trigger);
    }
}