using System;
using crypto.bot.backend.dto;
using crypto.bot.backend.Models.CryptoTrigger;
using Newtonsoft.Json;

namespace crypto.bot.backend.Services.TriggerServices.TriggerConverterService
{
    public class TriggerConverterService : ITriggerConverterService
    {
        public CryptoTrigger Parse(PostTriggerRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            switch (request.Type)
            {
                case "price":
                    return JsonConvert.DeserializeObject<PriceCryptoTrigger>(request.Trigger.ToString());
            }

            return null;
        }
    }
}