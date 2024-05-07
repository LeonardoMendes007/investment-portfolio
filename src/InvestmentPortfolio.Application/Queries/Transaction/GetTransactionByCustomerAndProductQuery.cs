using InvestmentPortfolio.Application.Responses.Details;
using MovieApp.MovieApi.Application.Queries;
using MediatR;
using InvestmentPortfolio.Application.Pagination.Interface;


namespace InvestmentPortfolio.Application.Queries.Transactions;
public class GetTransactionByCustomerAndProductQuery : PagedListQuery, IRequest<IPagedList<TransactionDetails>>
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
}
