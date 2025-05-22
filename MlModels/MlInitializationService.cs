using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OdontoPrevAPI.MlModels
{
    public class MlInitializationService : IHostedService
    {
        private MlService _mlService;
        private readonly IServiceProvider _serviceProvider;

        public MlInitializationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;            
            using (var scope = _serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
            {
                _mlService = scope.ServiceProvider.GetService<MlService>();
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _mlService.TrainModelFromDatabase();
                Console.WriteLine("ML model trained successfully on startup.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error training ML model on startup: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}