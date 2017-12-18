using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using crypto.bot.backend.Repositories.Currency;
using crypto.bot.backend.Services.CurrencySource;
using crypto.bot.backend.Services.TriggerServices.TriggerChecker;

namespace crypto.bot.backend.Background
{
    public class CryptoCheckHostedService : HostedService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencySource _currencySource;
        private readonly ITriggerCheckerService _triggerCheckerService;

        public CryptoCheckHostedService(
            ICurrencyRepository currencyRepository,
            ITriggerCheckerService triggerCheckerService,
            ICurrencySource currencySource)
        {
            _currencyRepository = currencyRepository;
            _triggerCheckerService = triggerCheckerService;
            _currencySource = currencySource;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!IsStoped)
                try
                {
                    var currencies = await _currencySource.GetAsync(ct).ConfigureAwait(false);

                    _currencyRepository.AddCurrencies(currencies);
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