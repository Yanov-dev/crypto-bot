using System;
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
            //_con.GetCollection<CryptoTrigger>().EnsureIndex(e => e.TelegramUserId);
        }

        public void AddTrigger<T>(T trigger) where T : CryptoTrigger
        {
            trigger.Id = Guid.NewGuid();
            trigger.CreateDate = DateTime.Now;
            _con.GetCollection<T>().Insert(trigger);
        }
    }
}