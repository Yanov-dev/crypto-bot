using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crypto.bot.backend.Extensions;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Repositories;
using crypto.bot.backend.Repositories.Trigger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    [Authorize]
    [Route("api/trigger")]
    public class TriggerController : Controller
    {
        private readonly ITriggerRepository _triggerRepository;

        public TriggerController(ITriggerRepository triggerRepository)
        {
            _triggerRepository = triggerRepository;
        }

        [HttpGet]
        public async Task<List<CryptoTrigger>> Get()
        {
            var id = this.GetTelegramUserId();

            return null;
        }

        [HttpPost]
        public async Task Post([FromBody] CryptoTrigger cryptoTrigger)
        {
            var userId = this.GetTelegramUserId();
            await Task.Run(() =>
            {
                cryptoTrigger.Id = Guid.NewGuid();
                cryptoTrigger.TelegramUserId = userId;
                _triggerRepository.AddTrigger(cryptoTrigger);
            });
        }
    }
}