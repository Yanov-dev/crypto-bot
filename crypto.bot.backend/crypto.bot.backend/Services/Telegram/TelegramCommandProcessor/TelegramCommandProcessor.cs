using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Services.Auth;
using crypto.bot.backend.Services.Telegram.TelegramApiService;
using crypto.bot.backend.Services.Token;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace crypto.bot.backend.Services.Telegram.TelegramCommandProcessor
{
    public class TelegramCommandProcessor : ITelegramCommandProcessor
    {
        private readonly IAuthService _authService;
        private readonly ITelegramApiService _telegramApiService;
        private readonly ITokenService _tokenService;
        private int _offset;

        public TelegramCommandProcessor(
            ITelegramApiService telegramApiService,
            IAuthService authService,
            ITokenService tokenService)
        {
            _telegramApiService = telegramApiService;
            _authService = authService;
            _tokenService = tokenService;
        }

        public async Task ProcessUpdatesAsync(CancellationToken ct)
        {
            var updates = await _telegramApiService.GetUpdatesAsync(_offset, ct).ConfigureAwait(false);

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

                _offset = update.Id + 1;
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
                var tokenId = Guid.NewGuid();

                _tokenService.Add(tokenId, jwt);

                var sb = new StringBuilder();
                sb.AppendLine("Magic link");
                sb.AppendLine();
                sb.AppendLine($"http://localhost:4200/callback/{tokenId}");

                await _telegramApiService.SendTextMessageAsync(chatId, sb.ToString()).ConfigureAwait(false);
            }
        }
    }
}