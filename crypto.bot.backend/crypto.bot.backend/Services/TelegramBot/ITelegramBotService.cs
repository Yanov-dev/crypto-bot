﻿using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using Telegram.Bot.Types;

namespace crypto.bot.backend.Services.TelegramBot
{
    public interface ITelegramBotService
    {
        Task SendAboutPriceTrigger(PriceCryptoTrigger trigger, CurrencyInfo currency);

        Task<Update[]> GetUpdatesAsync(int offset, CancellationToken ct);

        Task SendTextMessageAsync(long chatId, string message);
    }
}