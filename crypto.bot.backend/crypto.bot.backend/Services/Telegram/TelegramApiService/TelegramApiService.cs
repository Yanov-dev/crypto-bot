using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Options;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace crypto.bot.backend.Services.Telegram.TelegramApiService
{
    public class TelegramApiService : ITelegramApiService
    {
        private readonly TelegramBotClient _api;

        public TelegramApiService(IOptions<TelegramOptions> telegramOptions)
        {
            var token = telegramOptions?.Value?.Token;

            if (string.IsNullOrEmpty(token))
                throw new Exception("telegram token not set");

            _api = new TelegramBotClient(token);
        }

        public async Task SendAboutPriceTrigger(PriceCryptoTrigger trigger, CurrencyInfo currency)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{trigger.Currency} {trigger.Operator} {trigger.Price}$");
            sb.AppendLine($"Price of {trigger.Currency} is {currency.PriceUsd}$");

            await SendTextMessageAsync(trigger.TelegramUserId, sb.ToString()).ConfigureAwait(false);
        }

        public async Task<Update[]> GetUpdatesAsync(int offset, CancellationToken ct)
        {
            return await _api.GetUpdatesAsync(offset, cancellationToken: ct).ConfigureAwait(false);
        }

        public async Task SendTextMessageAsync(long chatId, string message)
        {
            await _api.SendTextMessageAsync(chatId, message);
        }
    }
}