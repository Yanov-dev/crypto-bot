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
        }

        public void UpdateCurrencies(CryptoInfo[] infos)
        {
            if (infos == null || infos.Length == 0)
                return;

            if (_con.CollectionExists(nameof(CryptoInfo)))
            {
                var col = _con.GetCollection<CryptoInfo>();
                col.Update(infos);
            }
            else
            {
                var col = _con.GetCollection<CryptoInfo>();
                col.InsertBulk(infos);
            }
        }

        public List<CryptoInfo> GetCurrencies()
        {
            return _con.GetCollection<CryptoInfo>().FindAll().ToList();
        }
    }
}