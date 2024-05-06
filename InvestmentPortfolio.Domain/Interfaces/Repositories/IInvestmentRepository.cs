using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Domain.Interfaces.Repositories;
public interface IInvestmentRepository
{
    Task SaveAsync(Investment investment);
}
