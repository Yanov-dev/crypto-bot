using System.Linq;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Repositories.Currency;
using crypto.bot.backend.Repositories.Trigger;
using crypto.bot.backend.Services.TelegramBot;

namespace crypto.bot.backend.Services.TriggerServices.TriggerChecker
{
    public class TriggerCheckerService : ITriggerCheckerService
    {
        private readonly ITriggerRepository _triggerRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ITelegramBotService _telegramBotService;

        public TriggerCheckerService(
            ITriggerRepository triggerRepository,
            ICurrencyRepository currencyRepository,
            ITelegramBotService telegramBotService)
        {
            _triggerRepository = triggerRepository;
            _currencyRepository = currencyRepository;
            _telegramBotService = telegramBotService;
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
                    _telegramBotService.SendAboutPriceTrigger(trigger, currency);
                    _triggerRepository.Remove<PriceCryptoTrigger>(trigger.Id);
                }
            }
        }
    }
}