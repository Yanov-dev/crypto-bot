using System.Collections.Generic;
using crypto.bot.backend.Models;

namespace crypto.bot.backend.Repositories.Currency
{
    public interface ICurrencyRepository
    {
        void AddCurrencies(CurrencyInfo[] infos);

        List<CurrencyInfo> GetCurrencies();
    }
}