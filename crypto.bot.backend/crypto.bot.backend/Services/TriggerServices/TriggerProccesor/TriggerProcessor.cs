using System;
using System.Collections.Generic;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Repositories.Trigger;

namespace crypto.bot.backend.Services.TriggerServices.TriggerProccesor
{
    public class TriggerProcessor : ITriggerProcessor
    {
        private readonly ITriggerRepository _triggerRepository;

        public TriggerProcessor(ITriggerRepository triggerRepository)
        {
            _triggerRepository = triggerRepository;
        }

        public void Delete(string type, Guid id)
        {
            switch (type)
            {
                case "price":
                    _triggerRepository.Delete<PriceCryptoTrigger>(id);
                    break;
            }
        }

        public void Save(CryptoTrigger trigger, long telegramUserId)
        {
            if (trigger == null)
                throw new ArgumentNullException(nameof(trigger));

            switch (trigger)
            {
                case PriceCryptoTrigger priceTrigger:
                    _triggerRepository.AddTrigger(priceTrigger, telegramUserId);
                    break;
            }
        }

        public IEnumerable<CryptoTrigger> GetUserTriggers(string type, long telegramUserId)
        {
            switch (type)
            {
                case "price":
                    return _triggerRepository.GetUserTriggers<PriceCryptoTrigger>(telegramUserId);
            }

            return null;
        }
    }
}