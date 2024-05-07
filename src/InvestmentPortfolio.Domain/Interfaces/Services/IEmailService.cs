using InvestmentPortfolio.Domain.Entities.Product;

namespace InvestmentPortfolio.Domain.Interfaces.Services;
public interface IEmailService
{
    Task SendproductExpiredEmailAsync(Product product);
}
