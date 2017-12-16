using System.Collections.Generic;
using System.Threading.Tasks;
using crypto.bot.backend.Models;
using crypto.bot.backend.Repositories;
using crypto.bot.backend.Repositories.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{    
    [Authorize]
    [Route("api/currency")]
    public class CurrencyController : Controller
    {
        public async Task<List<CurrencyInfo>> Get([FromServices] ICurrencyRepository cryptoRepository)
        {
            return await Task.Run(() => cryptoRepository.GetCurrencies());
        }
    }
}