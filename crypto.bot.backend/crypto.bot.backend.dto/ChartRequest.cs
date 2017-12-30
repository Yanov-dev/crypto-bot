using System;

namespace crypto.bot.backend.dto
{
    public class ChartRequest
    {
        public string PrimaryCurrency { get; set; }
        
        public string SecondaryCurrency { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
    }
}