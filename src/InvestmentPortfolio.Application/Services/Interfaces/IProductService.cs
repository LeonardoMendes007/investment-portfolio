using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface IProductService
{
    Task<Product> GetByIdAsync(Guid id);
    Task<IQueryable<Product>> GetAllAsync(bool inactive);
    Task<IQueryable<Transaction>> GetAllTransactionsAsync(Guid id);
    Task<Guid> CreateAsync(Product product);
    Task UpdateAsync(Product product);
}
