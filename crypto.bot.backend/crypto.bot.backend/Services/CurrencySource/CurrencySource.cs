using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using crypto.bot.backend.dto;
using crypto.bot.backend.Extensions;
using crypto.bot.backend.Models;
using Newtonsoft.Json;
using RestSharp;

namespace crypto.bot.backend.Services.CurrencySource
{
    public class CurrencySource : ICurrencySource
    {
        private class CurrencyHistoryResponse
        {
            [JsonProperty("price_usd")]
            public List<double[]> PriceUsd { get; set; }
        }

        public async Task<CurrencyInfo[]> GetAsync(CancellationToken ct)
        {
            var models = await Get<CurrencyDto[]>("https://api.coinmarketcap.com/v1/ticker/", ct).ConfigureAwait(false);

            return Mapper.Map<CurrencyInfo[]>(models);
        }

        public async Task<CurrencyHistory> GetHistory(string currencyName, DateTime from, DateTime to)
        {
            var url =
                $"https://graphs.coinmarketcap.com/currencies/{currencyName}/{from.ToUnixTime()}/{to.ToUnixTime()}/";

            var cth = await Get<CurrencyHistoryResponse>(url, CancellationToken.None).ConfigureAwait(false);

            var r = cth.PriceUsd;

            return null;
        }

        private async Task<T> Get<T>(string url, CancellationToken ct)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            var resp = client.Get(request);
            var response = await client.ExecuteTaskAsync(request, ct).ConfigureAwait(false);
            if (response.ErrorException != null)
                throw response.ErrorException;

            var json = response.Content;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}