using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Domain.Interfaces.Repositories;
public interface IProductRepository
{
    Task<Product> FindByIdAsync(Guid id);
    Task<Product> FindByNameAsync(string name);   
    IQueryable<Product> FindAll();
    IQueryable<Transaction> FindAllTransations(Guid id);
    Task SaveAsync(Product Product);
}
