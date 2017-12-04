using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using crypto.bot.backend.Models;
using crypto.bot.backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    [Authorize]
    [Route("api/currency")]
    public class TriggerController : Controller
    {
        private readonly ICryptoRepository _cryptoRepository;

        public TriggerController(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }

        [HttpGet]
        public async Task<List<CurrencyTrigger>> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await Task.Run(() => { return _cryptoRepository.GetTriggers(0); });
        }

        [HttpPost]
        public async Task Post([FromBody] CurrencyTrigger trigger)
        {
            await Task.Run(() => { _cryptoRepository.AddTrigger(trigger); });
        }
    }
}