using AutoMapper;
using InvestmentPortfolio.Application.Pagination;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Product;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class ProductQueryHandler : IRequestHandler<GetProductQuery, IPagedList<ProductSummary>>,
                                   IRequestHandler<GetProductByIdQuery, ProductDetails>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductQueryHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<IPagedList<ProductSummary>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(request.Inactive);

        var productsSummary = products
            .Select(p => new ProductSummary
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CurrentPrice = p.CurrentPrice,
                ExpirationDate = p.ExpirationDate,
                IsActive = (p.ExpirationDate >= DateTime.Now ? p.IsActive : false)
            });

        return PagedList<ProductSummary>.CreatePagedList(productsSummary, request.Page, request.PageSize);
    }

    public async Task<ProductDetails> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(request.Id);

        return _mapper.Map<ProductDetails>(product);
    }
}
