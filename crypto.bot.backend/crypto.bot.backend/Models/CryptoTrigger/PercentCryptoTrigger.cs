namespace crypto.bot.backend.Models.CryptoTrigger
{
    public class PercentCryptoTrigger : CryptoTrigger
    {
        public string CurrencyId { get; set; }
        
        public double PercentChange { get; set; }
    }
}