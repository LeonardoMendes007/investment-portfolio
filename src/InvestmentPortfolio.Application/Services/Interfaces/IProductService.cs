using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Domain.Entities.Product;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface IProductService
{
    Task<IQueryable<ProductSummary>> GetAllAsync(bool inactive);
    Task<ProductDetails> GetByIdAsync(Guid id);
    Task<IQueryable<TransactionDetails>> GetAllTransactionsAsync(Guid id);
    Task<Guid> CreateAsync(Product product);
    Task UpdateAsync(Product product);
}
