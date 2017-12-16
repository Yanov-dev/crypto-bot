using crypto.bot.backend.Models.CryptoTrigger;

namespace crypto.bot.backend.Repositories.Trigger
{
    public interface ITriggerRepository
    {
        void AddTrigger<T>(T trigger) where T : CryptoTrigger;
    }
}