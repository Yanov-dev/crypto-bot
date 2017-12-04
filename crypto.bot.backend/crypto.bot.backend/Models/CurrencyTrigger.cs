using System;

namespace crypto.bot.backend.Models
{
    public class CurrencyTrigger
    {
        public string Id { get; set; }
        
        public int TelegramUserId { get; set; }
        
        public string CurrencyId { get; set; }
        
        public CurrencyOperator Operator { get; set; }
        
        public float Price { get; set; }
        
        public string Description { get; set; }
    }
}