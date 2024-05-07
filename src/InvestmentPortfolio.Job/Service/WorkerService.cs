using InvestmentPortfolio.Domain.Interfaces.Services;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolio.Job.Service;
public class WorkerService : IWorkerService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly ILogger<WorkerService> _logger;

    public WorkerService(IUnitOfWork unitOfWork, IEmailService emailService, ILogger<WorkerService> logger)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task DoWorkAsync()
    {
        var productsQuery = _unitOfWork.ProductRepository.FindAll();

        var expiredProducts = productsQuery.Where(p => (p.ExpirationDate > DateTime.Now) && (p.ExpirationDate - DateTime.Now).Days < 5).ToList();

        _logger.LogInformation($"{expiredProducts.Count} expired products were found");

        foreach ( var product in expiredProducts )
        {
            await _emailService.SendproductExpiredEmailAsync(product);
        }
    }
}
