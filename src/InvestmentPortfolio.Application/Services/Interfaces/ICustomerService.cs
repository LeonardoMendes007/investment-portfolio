using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface ICustomerService
{
    Task<IPagedList<InvestmentSummary>> GetInvestmentByQueryAsync(Guid id, int page, int pageSize);
    Task<InvestmentDetails> GetInvestmentByProductAsync(Guid id, Guid productId);
    Task<IPagedList<TransactionSummary>> GetTransactionByQueryAsync(Guid id, int page, int pageSize);
    Task<IPagedList<TransactionDetails>> GetTransactionsByProductAsync(Guid id, Guid productId, int page, int pageSize);
}
