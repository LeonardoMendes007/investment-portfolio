using AutoMapper;
using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.CommandHandlers;
public class ProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>,
                                     IRequestHandler<UpdateProductCommand>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    private readonly IValidator<CreateProductCommand> _validatorCreateProductCommand;
    private readonly IValidator<UpdateProductCommand> _validatorUpdateProductCommand;

    public ProductCommandHandler(IProductService productService, IMapper mapper, IValidator<CreateProductCommand> validatorCreateProductCommand, IValidator<UpdateProductCommand> validatorUpdateProductCommand)
    {
        _productService = productService;
        _mapper = mapper;
        _validatorCreateProductCommand = validatorCreateProductCommand;
        _validatorUpdateProductCommand = validatorUpdateProductCommand;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validatorCreateProductCommand.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.ToDictionary());
        }

        var product = _mapper.Map<Product>(request);

        var productId = await _productService.CreateAsync(product);

        return productId;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validatorUpdateProductCommand.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var product = _mapper.Map<Product>(request);

        await _productService.UpdateAsync(product);
    }
}
