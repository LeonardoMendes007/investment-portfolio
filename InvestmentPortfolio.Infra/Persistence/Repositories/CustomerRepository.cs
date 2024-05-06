using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolio.Infra.Persistence.Repositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly InvestimentPortfolioDbContext _investimentPortfolioDbContext;
    public CustomerRepository(InvestimentPortfolioDbContext movieAppDbContext)
    {
        _investimentPortfolioDbContext = movieAppDbContext;
    }

    public async Task<Customer> FindByIdAsync(Guid id)
    {
        return await _investimentPortfolioDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Investment> FindInvestimentByProductIdAsync(Guid id, Guid productId)
    {
        return await _investimentPortfolioDbContext.Investiments.FirstOrDefaultAsync(i => i.CustomerId == id && i.ProductId == productId);
    }

    public IQueryable<Transaction> FindTransactionsByProductId(Guid id, Guid productId)
    {
        return _investimentPortfolioDbContext.Transactions.Where(i => i.CustomerId == id && i.ProductId == productId).AsQueryable();
    }

    public IQueryable<Investment> FindAllInvestments(Guid id)
    {
        return _investimentPortfolioDbContext.Investiments.Where(c => c.CustomerId == id).AsQueryable();
    }

    public IQueryable<Transaction> FindAllTransactions(Guid id)
    {
        return _investimentPortfolioDbContext.Transactions.Where(c => c.CustomerId == id).AsQueryable();
    }

    public async Task SaveAsync(Customer customer)
    {
        await _investimentPortfolioDbContext.Customers.AddAsync(customer);
    }

    
}
