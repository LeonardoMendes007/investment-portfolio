using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface ICustomerService
{
    Task<IPagedList<InvestmentSummary>> GetInvestmentByQuery(Guid id, int page, int pageSize);
    Task<InvestmentDetails> GetInvestmentByProduct(Guid id, Guid productId);
    Task<IPagedList<TransactionSummary>> GetTransactionByQuery(Guid id, int page, int pageSize);
    Task<IPagedList<TransactionDetails>> GetTransactionsByProduct(Guid id, Guid productId, int page, int pageSize);
}
