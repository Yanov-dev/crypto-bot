using System;
using System.Collections.Generic;
using System.Linq;
using crypto.bot.backend.Models;
using crypto.bot.backend.Options;
using LiteDB;
using Microsoft.Extensions.Options;

namespace crypto.bot.backend.Repositories.Currency
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly LiteDatabase _con;

        public CurrencyRepository(IOptions<RepositoryOptions> options)
        {
            var dbPath = options.Value.CurrencyDbPath;
            if (string.IsNullOrEmpty(dbPath))
                throw new ArgumentNullException($"{nameof(options.Value.CurrencyDbPath)} not set");
            
            _con = new LiteDatabase(dbPath);
        }

        public void AddCurrencies(CurrencyInfo[] infos)
        {
            if (infos == null || infos.Length == 0)
                return;

            if (_con.CollectionExists(nameof(CurrencyInfo)))
            {
                var col = _con.GetCollection<CurrencyInfo>();
                col.Update(infos);
            }
            else
            {
                var col = _con.GetCollection<CurrencyInfo>();
                col.InsertBulk(infos);
            }
        }

        public List<CurrencyInfo> GetCurrencies()
        {
            return _con.GetCollection<CurrencyInfo>().FindAll().ToList();
        }
    }
}