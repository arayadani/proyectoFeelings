using Microsoft.AspNetCore.Mvc;

namespace proyectoFeelings.Services
{
    public class InventoryBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InventoryBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var inventoryService =
                        scope.ServiceProvider
                        .GetRequiredService<InventoryCheckService>();

                    await inventoryService.CheckInventoryAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

}
