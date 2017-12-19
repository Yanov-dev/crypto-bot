using System;

namespace crypto.bot.backend.dto
{
    public class DeleteTriggerRequest
    {
        public string Type { get; set; }
        
        public Guid Id { get; set; }
    }
}