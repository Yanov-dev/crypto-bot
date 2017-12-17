using Newtonsoft.Json.Linq;

namespace crypto.bot.backend.dto
{
    public class PostTriggerRequest
    {
        public string Type { get; set; }
        
        public JObject Trigger { get; set; }
    }
}