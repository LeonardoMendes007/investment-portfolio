using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface IInvestmentService
{
    Task<Investment> BuyInvestmentAsync(Transaction transaction);
    Task<Investment> SellInvestmentAsync(Transaction transaction);
    
}
