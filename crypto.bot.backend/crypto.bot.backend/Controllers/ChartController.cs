using System;
using System.Threading.Tasks;
using crypto.bot.backend.dto;
using crypto.bot.backend.Services.CurrencySource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    [Route("api/chart")]
    public class ChartController : Controller
    {
        [HttpPost]
        public async Task<ChartResponse> Get(
            [FromBody] ChartRequest chartRequest,
            [FromServices] ICurrencySource currencySource)
        {
            var res = await currencySource.GetHistory(
                "verge",
                DateTime.UtcNow.AddDays(-1), 
                DateTime.UtcNow);
            return null;
        }
    }
}