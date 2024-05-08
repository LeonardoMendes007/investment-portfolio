using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface ICustomerService
{
    Task<IQueryable<InvestmentSummary>> GetInvestmentByQueryAsync(Guid id);
    Task<InvestmentDetails> GetInvestmentByProductAsync(Guid id, Guid productId);
    Task<IQueryable<TransactionSummary>> GetTransactionByQueryAsync(Guid id);
    Task<IQueryable<TransactionDetails>> GetTransactionsByProductAsync(Guid id, Guid productId);
}
