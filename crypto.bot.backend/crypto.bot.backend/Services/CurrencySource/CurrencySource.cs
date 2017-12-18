using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using crypto.bot.backend.dto;
using crypto.bot.backend.Models;
using Newtonsoft.Json;
using RestSharp;

namespace crypto.bot.backend.Services.CurrencySource
{
    public class CurrencySource : ICurrencySource
    {
        private readonly RestClient _restClient;

        public CurrencySource()
        {
            _restClient = new RestClient("https://api.coinmarketcap.com");
        }

        public async Task<CurrencyInfo[]> GetAsync(CancellationToken ct)
        {
            var request = new RestRequest("v1/ticker/", Method.GET);

            var response = await _restClient.ExecuteTaskAsync(request, ct).ConfigureAwait(false);
            if (response.ErrorException != null)
                throw response.ErrorException;

            var json = response.Content;
            var models = JsonConvert.DeserializeObject<CurrencyDto[]>(json);

            return Mapper.Map<CurrencyInfo[]>(models);
        }
    }
}