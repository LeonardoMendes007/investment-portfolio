using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Responses.Summary;
using MediatR;
using MovieApp.MovieApi.Application.Queries;

namespace InvestmentPortfolio.Application.Queries.Product;
public class GetProductQuery : PagedListQuery, IRequest<IPagedList<ProductSummary>>
{
    public string GetKey()
    {
        return $"p:{Page}&ps:{PageSize}";
    }
}
