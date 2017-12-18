namespace crypto.bot.backend.Models
{
    /*{
        "id": "bitcoin",
        "name": "Bitcoin",
        "symbol": "BTC",
        "rank": "1",
        "price_usd": "11777.6",
        "price_btc": "1.0",
        "24h_volume_usd": "5505890000.0",
        "market_cap_usd": "196893935971",
        "available_supply": "16717662.0",
        "total_supply": "16717662.0",
        "max_supply": "21000000.0",
        "percent_change_1h": "0.08",
        "percent_change_24h": "6.24",
        "percent_change_7d": "26.3",
        "last_updated": "1512330854"
    },*/

    public class CurrencyInfo
    {
        public string AvailableSupply { get; set; }

        public string Id { get; set; }

        public string LastUpdated { get; set; }

        public string MarketCapUsd { get; set; }

        public string MaxSupply { get; set; }

        public string Name { get; set; }

        public string PercentChange1h { get; set; }

        public string PercentChange24h { get; set; }

        public string PercentChange7d { get; set; }

        public string PriceBtc { get; set; }

        public float PriceUsd { get; set; }

        public int Rank { get; set; }

        public string Symbol { get; set; }

        public string The24hVolumeUsd { get; set; }

        public string TotalSupply { get; set; }
    }
}