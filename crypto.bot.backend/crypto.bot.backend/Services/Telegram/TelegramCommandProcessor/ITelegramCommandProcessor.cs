using System.Threading;
using System.Threading.Tasks;

namespace crypto.bot.backend.Services.Telegram.TelegramCommandProcessor
{
    public interface ITelegramCommandProcessor
    {
        Task ProcessUpdatesAsync(CancellationToken ct);
    }
}