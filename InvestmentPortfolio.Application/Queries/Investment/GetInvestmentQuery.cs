using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Summary;
using MediatR;
using MovieApp.MovieApi.Application.Queries;

namespace InvestmentPortfolio.Application.Queries.Investments;
public class GetInvestmentQuery : PagedListQuery, IRequest<IPagedList<InvestmentSummary>>
{
    public Guid CustomerId { get; set; }
}
