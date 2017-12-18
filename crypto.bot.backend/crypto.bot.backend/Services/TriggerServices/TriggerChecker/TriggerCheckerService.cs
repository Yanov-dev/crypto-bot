using System.Linq;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Repositories.Currency;
using crypto.bot.backend.Repositories.Trigger;
using crypto.bot.backend.Services.Telegram.TelegramApiService;

namespace crypto.bot.backend.Services.TriggerServices.TriggerChecker
{
    public class TriggerCheckerService : ITriggerCheckerService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ITelegramApiService _telegramApiService;
        private readonly ITriggerRepository _triggerRepository;

        public TriggerCheckerService(
            ITriggerRepository triggerRepository,
            ICurrencyRepository currencyRepository,
            ITelegramApiService telegramApiService)
        {
            _triggerRepository = triggerRepository;
            _currencyRepository = currencyRepository;
            _telegramApiService = telegramApiService;
        }

        public void Check()
        {
            var currencies = _currencyRepository.GetCurrencies().ToDictionary(e => e.Name);
            foreach (var trigger in _triggerRepository.GetAll<PriceCryptoTrigger>())
            {
                var currency = currencies[trigger.Currency];

                var condition = trigger.Operator == CurrencyOperator.MoreThan
                    ? trigger.Price < currency.PriceUsd
                    : trigger.Price > currency.PriceUsd;

                if (condition)
                {
                    _telegramApiService.SendAboutPriceTrigger(trigger, currency);
                    _triggerRepository.Remove<PriceCryptoTrigger>(trigger.Id);
                }
            }
        }
    }
}