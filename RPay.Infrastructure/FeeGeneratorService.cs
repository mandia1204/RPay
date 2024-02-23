using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RPay.Infrastructure.Database.Repositories;

namespace RPay.Infrastructure
{
    public sealed class FeeGeneratorService: IHostedService, IAsyncDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public FeeGeneratorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        private readonly Task _completedTask = Task.CompletedTask;
        private Timer? _timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return _completedTask;
        }

        private async void UpdateFee(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var feeRepository = scope.ServiceProvider.GetService<IFeeRepository>();
                var latestFee = await feeRepository.GetLatestFee();
                var min = 0;
                var max = 2;

                var random = new Random();
                var randomValue = (decimal)(random.NextDouble() * (max - min) + min);
                var newFee = latestFee.HasValue ? latestFee.Value * randomValue : randomValue;

                await feeRepository.AddFee(newFee);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_timer is IAsyncDisposable timer)
            {
                await timer.DisposeAsync();
            }

            _timer = null;
        }
    }
}
