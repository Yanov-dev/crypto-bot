using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Models;

namespace crypto.bot.backend.Services.CurrencySource
{
    public interface ICurrencySource
    {
        Task<CurrencyInfo[]> GetAsync(CancellationToken ct);
    }
}