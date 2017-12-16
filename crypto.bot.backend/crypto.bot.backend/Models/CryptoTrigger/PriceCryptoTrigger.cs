namespace crypto.bot.backend.Models.CryptoTrigger
{
    public class PriceCryptoTrigger : CryptoTrigger
    {
        public string CurrencyId { get; set; }
        
        public CurrencyOperator Operator { get; set; }
        
        public float Price { get; set; }
    }
}