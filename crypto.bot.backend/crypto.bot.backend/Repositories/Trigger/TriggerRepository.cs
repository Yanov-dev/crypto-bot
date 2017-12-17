using System;
using System.Collections.Generic;
using System.Linq;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Options;
using LiteDB;
using Microsoft.Extensions.Options;

namespace crypto.bot.backend.Repositories.Trigger
{
    public class TriggerRepository : ITriggerRepository
    {
        private readonly LiteDatabase _con;

        public TriggerRepository(IOptions<RepositoryOptions> options)
        {
            var dbPath = options.Value.TriggerDbPath;
            if (string.IsNullOrEmpty(dbPath))
                throw new ArgumentNullException($"{nameof(options.Value.TriggerDbPath)} not set");

            _con = new LiteDatabase(dbPath);
            _con.GetCollection<PriceCryptoTrigger>().EnsureIndex(e => e.TelegramUserId);
        }

        public void AddTrigger<T>(T trigger, long telegramUserId) where T : CryptoTrigger
        {
            trigger.Id = Guid.NewGuid();
            trigger.CreateDate = DateTime.Now;
            trigger.TelegramUserId = telegramUserId;
            _con.GetCollection<T>().Insert(trigger);
        }

        public IEnumerable<T> GetUserTriggers<T>(long telegramUserId) where T : CryptoTrigger
        {
            return _con.GetCollection<T>().Find(e => e.TelegramUserId == telegramUserId).ToList();
        }
    }
}