using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using crypto.bot.backend.dto;
using crypto.bot.backend.Models;
using crypto.bot.backend.Repositories;
using crypto.bot.backend.Repositories.Currency;
using crypto.bot.backend.Services.TriggerServices.TriggerChecker;
using Newtonsoft.Json;
using RestSharp;

namespace crypto.bot.backend.Background
{
    public class CryptoCheckHostedService : HostedService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ITriggerCheckerService _triggerCheckerService;

        public CryptoCheckHostedService(
            ICurrencyRepository currencyRepository,
            ITriggerCheckerService triggerCheckerService)
        {
            _currencyRepository = currencyRepository;
            _triggerCheckerService = triggerCheckerService;
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
                    var models = JsonConvert.DeserializeObject<CurrencyDto[]>(json);

                    _currencyRepository.AddCurrencies(Mapper.Map<CurrencyInfo[]>(models));
                }
                catch (Exception ex)
                {
                    // todo
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    _triggerCheckerService.Check();
                    await Task.Delay(5000, ct).ConfigureAwait(false);                    
                }
            }
        }
    }
}