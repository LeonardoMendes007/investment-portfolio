using InvestmentPortfolio.Domain.Entities.Product;

namespace InvestmentPortfolio.Domain.Interfaces.Repositories;
public interface IProductRepository
{
    Task<Product> FindByIdAsync(Guid id);
    Task<Product> FindByNameAsync(string name);
    IQueryable<Product> FindAll();
    Task SaveAsync(Product Product);
}
