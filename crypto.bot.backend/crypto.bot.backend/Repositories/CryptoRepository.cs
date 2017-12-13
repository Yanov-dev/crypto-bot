using System;
using System.Collections.Generic;
using System.Linq;
using crypto.bot.backend.Models;
using LiteDB;

namespace crypto.bot.backend.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly LiteDatabase _con;

        public CryptoRepository()
        {
            _con = new LiteDatabase("main.db4");
            _con.GetCollection<CurrencyTrigger>().EnsureIndex(e => e.TelegramUserId);
        }

        public void UpdateCurrencies(CurrencyInfo[] infos)
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

        public void AddTrigger(CurrencyTrigger trigger)
        {
            trigger.Id = Guid.NewGuid().ToString();
            _con.GetCollection<CurrencyTrigger>().Insert(trigger);
        }

        public List<CurrencyTrigger> GetTriggers(long telegramUserId)
        {
            return _con.GetCollection<CurrencyTrigger>().Find(e => e.TelegramUserId == telegramUserId).ToList();
        }
    }
}