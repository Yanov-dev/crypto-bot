using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crypto.bot.backend.dto;
using crypto.bot.backend.Extensions;
using crypto.bot.backend.Models;
using crypto.bot.backend.Models.CryptoTrigger;
using crypto.bot.backend.Repositories;
using crypto.bot.backend.Repositories.Trigger;
using crypto.bot.backend.Services.TriggerServices.TriggerConverterService;
using crypto.bot.backend.Services.TriggerServices.TriggerProccesor;
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
        public async Task<List<CryptoTrigger>> Get(
            string type,
            [FromServices] ITriggerProcessor triggerProcessor)
        {
            var id = this.GetTelegramUserId();

            return await Task.Run(() => triggerProcessor.GetUserTriggers(type, id).ToList());
        }

        [HttpPost]
        public async Task Post(
            [FromBody] PostTriggerRequest request,
            [FromServices] ITriggerConverterService triggerConverterService,
            [FromServices] ITriggerProcessor triggerProcessor)
        {
            var userId = this.GetTelegramUserId();

            await Task.Run(() =>
            {
                var trigger = triggerConverterService.Parse(request);

                triggerProcessor.Save(trigger, userId);
            });
        }
    }
}