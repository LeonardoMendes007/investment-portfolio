using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using MediatR;
using MovieApp.MovieApi.Application.Queries;

namespace InvestmentPortfolio.Application.Queries.Transaction;
public class GetTransactionByProductQuery : PagedListQuery, IRequest<IPagedList<TransactionDetails>>
{
    public Guid ProductId { get; set; }
}
