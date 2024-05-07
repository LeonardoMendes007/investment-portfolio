using InvestmentPortfolio.Domain.Interfaces.Services;
using InvestmentPortfolio.Job.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace InvestmentPortfolio.Job;
public class InfoWorker : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly WorkerOptions _workerOptions;
    private readonly ILogger<InfoWorker> _logger;
    private Timer _timer;

    public InfoWorker(ILogger<InfoWorker> logger, IServiceScopeFactory serviceScopeFactory, WorkerOptions workerOptions)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _workerOptions = workerOptions;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker is starting...");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(_workerOptions.IntervalInMinutes ));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        try
        {
            _logger.LogInformation("Start of the verification process for products close to expiration...");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var workerService = scope.ServiceProvider.GetRequiredService<IWorkerService>();

                workerService.DoWorkAsync();
            }

            _logger.LogInformation("Finish of the verification process for products close to expiration...");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"WorkerInfo => {ex.Message}");
        }        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker is stopping...");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
