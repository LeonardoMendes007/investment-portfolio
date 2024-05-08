using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface ICustomerService
{
    Task<IQueryable<Investment>> GetInvestmentByQueryAsync(Guid id);
    Task<Investment> GetInvestmentByProductAsync(Guid id, Guid productId);
    Task<IQueryable<Transaction>> GetTransactionByQueryAsync(Guid id);
    Task<IQueryable<Transaction>> GetTransactionsByProductAsync(Guid id, Guid productId);
}
