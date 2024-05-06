using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Domain.Interfaces.Repositories;
public interface ITransactionRepository
{
    Task SaveAsync(Transaction transaction);
}
