using System;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Options;
using crypto.bot.backend.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace crypto.bot.backend.Background
{
    public class TelegramBotHostedService : HostedService
    {
        private readonly IAuthService _authService;
        private readonly TelegramBotClient _api;

        public TelegramBotHostedService(IAuthService authService, IOptions<TelegramOptions> telegramOptions)
        {
            _authService = authService;
            var token = telegramOptions?.Value?.Token;

            if (string.IsNullOrEmpty(token))
                throw new Exception("telegram token not set");

            _api = new TelegramBotClient(token);
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            var offset = 0;

            while (!IsStoped)
            {
                var updates = await _api.GetUpdatesAsync(offset, cancellationToken: ct).ConfigureAwait(false);

                foreach (var update in updates)
                {
                    try
                    {
                        await ProcessUpdate(update).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    offset = update.Id + 1;
                }

                await Task.Delay(500, ct).ConfigureAwait(false);
            }
        }

        private async Task ProcessUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate)
                return;

            var chatId = update.Message.Chat.Id;

            var message = update.Message.Text;

            if ("/login".Equals(message))
            {
                var jwt = _authService.GenerateJwt(chatId);
                await _api.SendTextMessageAsync(chatId, jwt).ConfigureAwait(false);
            }
        }
    }
}