using System;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Services.Telegram.TelegramCommandProcessor;

namespace crypto.bot.backend.Background
{
    public class TelegramBotHostedService : HostedService
    {
        private readonly ITelegramCommandProcessor _telegramCommandProcessor;

        public TelegramBotHostedService(ITelegramCommandProcessor telegramCommandProcessor)
        {
            _telegramCommandProcessor = telegramCommandProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!IsStoped)
            {
                try
                {
                    await _telegramCommandProcessor.ProcessUpdatesAsync(ct).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(500, ct).ConfigureAwait(false);
            }
        }
    }
}