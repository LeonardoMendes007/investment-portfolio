using AutoMapper;
using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Handlers.CommandHandlers;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Validators.Product;
using InvestmentPortfolio.Domain.Entities.Product;
using Moq;

namespace InvestmentPortfolio.Application.Tests.Handlers.CommandHandlers;
public class ProductCommandHandlerTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<IMapper> _mockMapper;

    private readonly CreateProductCommandValidator _validatorCreateProductCommand = new CreateProductCommandValidator();
    private readonly UpdateProductCommandValidator _validatorUpdateProductCommand = new UpdateProductCommandValidator();

    private readonly ProductCommandHandler productCommandHandler;

    public ProductCommandHandlerTest()
    {
        _mockProductService = _mockRepository.Create<IProductService>();
        _mockMapper = _mockRepository.Create<IMapper>();

        productCommandHandler = new ProductCommandHandler(_mockProductService.Object, _mockMapper.Object, _validatorCreateProductCommand, _validatorUpdateProductCommand);
    }

    [Fact]
    public async Task Handle_CreateProductCommand_ReturnsGuid()
    {
        // Arrange
        var createCommand = new CreateProductCommand
        {
            Name = "PETR4",
            Description = "Petrobras",
            Price = 5,
            ExpirationDate = DateTime.UtcNow.AddYears(1),
            IsActive = true
        };

        _mockMapper
            .Setup(mapper => mapper.Map<Product>(createCommand))
            .Returns(new Product
            {
                Name = createCommand.Name,
                Description = createCommand.Description,
                InitialPrice = createCommand.Price,
                CurrentPrice = createCommand.Price,
                ExpirationDate = createCommand.ExpirationDate,
                IsActive = createCommand.IsActive
            });

        var guid = Guid.NewGuid();

        _mockProductService
            .Setup(service => service.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(guid);

        // Act
        var actual = await productCommandHandler.Handle(createCommand, CancellationToken.None);

        //Assert
        Assert.Equal(guid, actual);
    }

    [Fact]
    public async Task Handle_UpdateProductCommand_ReturnsGuid()
    {
        // Arrange
        var updateCommand = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "PETR4",
            Description = "Petrobras",
            InitialPrice = 5,
            CurrentPrice = 5,
            ExpirationDate = DateTime.UtcNow.AddYears(1),
            IsActive = true
        };

        _mockMapper
            .Setup(mapper => mapper.Map<Product>(updateCommand))
            .Returns(new Product
            {
                Id= Guid.NewGuid(),
                Name = updateCommand.Name,
                Description = updateCommand.Description,
                InitialPrice = updateCommand.InitialPrice,
                CurrentPrice = updateCommand.CurrentPrice,
                ExpirationDate = updateCommand.ExpirationDate,
                IsActive = updateCommand.IsActive
            });

        var guid = Guid.NewGuid();

        _mockProductService
            .Setup(service => service.UpdateAsync(It.IsAny<Product>()));

        // Act
        await productCommandHandler.Handle(updateCommand, CancellationToken.None);
    }
}
