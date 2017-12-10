using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crypto.bot.backend.Extensions;
using crypto.bot.backend.Models;
using crypto.bot.backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    [Authorize]
    [Route("api/trigger")]
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
            var id = this.GetTelegramUserId();

            return await Task.Run(() => _cryptoRepository.GetTriggers(id));
        }

        [HttpPost]
        public async Task Post([FromBody] CurrencyTrigger trigger)
        {
            var userId = this.GetTelegramUserId();
            await Task.Run(() =>
            {
                trigger.Id = Guid.NewGuid().ToString();
                trigger.TelegramUserId = userId;
                _cryptoRepository.AddTrigger(trigger);
            });
        }
    }
}