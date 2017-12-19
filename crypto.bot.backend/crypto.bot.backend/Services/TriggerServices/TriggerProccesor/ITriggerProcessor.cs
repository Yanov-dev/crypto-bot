using System;
using System.Collections.Generic;
using crypto.bot.backend.Models.CryptoTrigger;

namespace crypto.bot.backend.Services.TriggerServices.TriggerProccesor
{
    public interface ITriggerProcessor
    {
        void Delete(string type, Guid id);
        
        void Save(CryptoTrigger trigger, long telegramUserId);

        IEnumerable<CryptoTrigger> GetUserTriggers(string type, long telegramUserId);
    }
}