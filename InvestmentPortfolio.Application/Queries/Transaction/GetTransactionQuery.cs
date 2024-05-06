using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Summary;
using MediatR;
using MovieApp.MovieApi.Application.Queries;

namespace InvestmentPortfolio.Application.Queries.Transactions;
public class GetTransactionQuery : PagedListQuery, IRequest<IPagedList<TransactionSummary>>
{
    public Guid CustomerId { get; set; }
}
