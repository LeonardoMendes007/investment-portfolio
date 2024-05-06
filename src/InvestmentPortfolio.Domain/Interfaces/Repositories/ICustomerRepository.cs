using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Domain.Interfaces.Repositories;
public interface ICustomerRepository
{
    Task<Customer> FindByIdAsync(Guid id);
    IQueryable<Investment> FindAllInvestments(Guid id);
    Task<Investment> FindInvestimentByProductIdAsync(Guid id, Guid productId);
    IQueryable<Transaction> FindAllTransactions(Guid id);
    IQueryable<Transaction> FindTransactionsByProductId(Guid id, Guid productId);
    Task SaveAsync(Customer customer);
}
