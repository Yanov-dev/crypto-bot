using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace crypto.bot.backend.Background
{
    public abstract class HostedService : IHostedService
    {
        private CancellationTokenSource _cts;
        private Task _executingTask;

        public bool IsStoped => _cts.IsCancellationRequested;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
                return;

            _cts.Cancel();

            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken ct);
    }
}