using System.IO;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Options;
using crypto.bot.backend.Repositories.Trigger;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace crypto.bot.backend.tests
{
    [TestClass]
    public class TriggerRepositoryTests
    {
        [TestMethod]
        public void AddTest()
        {
            var dir = Directory.GetCurrentDirectory();
            var options = Microsoft.Extensions.Options.Options.Create(new RepositoryOptions
            {
                TriggerDbPath = "tests.db4"
            });
            var repo = new TriggerRepository(options);

            repo.AddTrigger(new PriceCryptoTrigger(), 0);
            repo.AddTrigger(new PercentCryptoTrigger(), 0);
        }
    }
}