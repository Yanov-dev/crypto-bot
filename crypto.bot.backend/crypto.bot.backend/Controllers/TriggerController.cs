﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.bot.backend.dto;
using crypto.bot.backend.Extensions;
using crypto.bot.backend.Models.CryptoTrigger;
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

        [HttpDelete]
        public async Task Delete(
            [FromBody] DeleteTriggerRequest request,
            [FromServices] ITriggerProcessor triggerProcessor)
        {
            // todo : check if this user can delelet this trigger
            
            await Task.Run(() =>
            {
                triggerProcessor.Delete(request.Type, request.Id);
            });
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