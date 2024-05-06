using InvestmentPortfolio.Application.Responses.Details;
using MediatR;
using MovieApp.MovieApi.Application.Queries;

namespace InvestmentPortfolio.Application.Queries.Investments;
public class GetInvestmentByProductQuery : PagedListQuery, IRequest<InvestmentDetails>
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
}
