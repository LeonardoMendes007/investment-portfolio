using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolio.Infra.Persistence.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly InvestimentPortfolioDbContext _investimentPortfolioDbContext;
    public ProductRepository(InvestimentPortfolioDbContext movieAppDbContext)
    {
        _investimentPortfolioDbContext = movieAppDbContext;
    }

    public IQueryable<Product> FindAll()
    {
        return _investimentPortfolioDbContext.Products.AsQueryable();
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        return await _investimentPortfolioDbContext.Products.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Product> FindByNameAsync(string name)
    {
        return await _investimentPortfolioDbContext.Products.FirstOrDefaultAsync(f => f.Name == name);
    }

    public async Task SaveAsync(Product Product)
    {
        await _investimentPortfolioDbContext.Products.AddAsync(Product);
    }
}
