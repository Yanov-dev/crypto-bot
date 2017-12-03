using Newtonsoft.Json;

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
    
    public class CryptoInfo
    {
        [JsonProperty("available_supply")]
        public string AvailableSupply { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("market_cap_usd")]
        public string MarketCapUsd { get; set; }

        [JsonProperty("max_supply")]
        public string MaxSupply { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent_change_1h")]
        public string PercentChange1h { get; set; }

        [JsonProperty("percent_change_24h")]
        public string PercentChange24h { get; set; }

        [JsonProperty("percent_change_7d")]
        public string PercentChange7d { get; set; }

        [JsonProperty("price_btc")]
        public string PriceBtc { get; set; }

        [JsonProperty("price_usd")]
        public string PriceUsd { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("24h_volume_usd")]
        public string The24hVolumeUsd { get; set; }

        [JsonProperty("total_supply")]
        public string TotalSupply { get; set; }
    }
}