using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Models;
using crypto.bot.backend.Repositories;
using Newtonsoft.Json;
using RestSharp;

namespace crypto.bot.backend.Background
{
    public class CryptoCheckHostedService : HostedService
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoCheckHostedService(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }
        
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!IsStoped)
            {
                try
                {
                    var client = new RestClient("https://api.coinmarketcap.com");

                    var request = new RestRequest("v1/ticker/", Method.GET);

                    var response = await client.ExecuteTaskAsync(request, ct).ConfigureAwait(false);
                    if (response.ErrorException != null)
                        throw response.ErrorException;
                    
                    var json = response.Content;
                    var models = JsonConvert.DeserializeObject<CryptoInfo[]>(json);

                    _cryptoRepository.UpdateCurrencies(models);
                }
                catch (Exception ex)
                {
                    // todo
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    await Task.Delay(5000, ct).ConfigureAwait(false);                    
                }
            }
        }
    }
}