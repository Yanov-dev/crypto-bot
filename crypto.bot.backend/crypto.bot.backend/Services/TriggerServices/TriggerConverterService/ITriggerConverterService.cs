using crypto.bot.backend.dto;
using crypto.bot.backend.Models.CryptoTrigger;

namespace crypto.bot.backend.Services.TriggerServices.TriggerConverterService
{
    public interface ITriggerConverterService
    {
        CryptoTrigger Parse(PostTriggerRequest request);
    }
}