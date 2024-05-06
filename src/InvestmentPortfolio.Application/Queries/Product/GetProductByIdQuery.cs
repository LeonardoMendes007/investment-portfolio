using InvestmentPortfolio.Application.Responses.Details;
using MediatR;

namespace InvestmentPortfolio.Application.Queries.Product;
public class GetProductByIdQuery : IRequest<ProductDetails>
{
    public Guid Id { get; set; }
}
