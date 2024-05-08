using InvestmentPortfolio.Application.Pagination;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Product;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class ProductQueryHandler : IRequestHandler<GetProductQuery, IPagedList<ProductSummary>>,
                                   IRequestHandler<GetProductByIdQuery, ProductDetails>
{
    private readonly IProductService _productService;

    public ProductQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IPagedList<ProductSummary>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productsSummary = await _productService.GetAllAsync(request.Inactive);

        return PagedList<ProductSummary>.CreatePagedList(productsSummary, request.Page, request.PageSize);
    }

    public async Task<ProductDetails> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetByIdAsync(request.Id);
    }
}
