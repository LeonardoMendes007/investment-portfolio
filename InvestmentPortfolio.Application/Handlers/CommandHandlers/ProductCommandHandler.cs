using AutoMapper;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.CommandHandlers;
public class ProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>,
                                              IRequestHandler<UpdateProductCommand>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductCommandHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        var productId = await _productService.CreateAsync(product);

        return productId;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _productService.UpdateAsync(product);
    }
}
