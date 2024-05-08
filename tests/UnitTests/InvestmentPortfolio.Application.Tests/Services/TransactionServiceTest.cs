using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;
using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;

namespace InvestmentPortfolio.Application.Tests.Services;
public class TransactionTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly ITransactionService _transactionService;

    public TransactionTest()
    {
        _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();

        _transactionService = new TransactionService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidTransaction_ReturnsTransaction()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var customer = new Customer
        {
            Id = customerId
        };
        var product = new Product
        {
            Id = productId,
            CurrentPrice = 100
        };
        var transaction = new Transaction
        {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = 5,
            Date = DateTime.UtcNow
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(customer);

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(product);

        _mockUnitOfWork.Setup(x => x.TransactionRepository.SaveAsync(It.IsAny<Transaction>()));

        // Act
        var result = await _transactionService.Create(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerId, result.CustomerId);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(transaction.Quantity, result.Quantity);
        Assert.Equal(product.CurrentPrice, result.PU);
    }

    [Fact]
    public async Task CreateAsync_InvalidTransactionCustomerNotFound_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        var customer = new Customer
        {
            Id = customerId
        };

        var transaction = new Transaction
        {
            CustomerId = customerId,
            Quantity = 5,
            Date = DateTime.UtcNow
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _transactionService.Create(transaction);
        });
    }

    [Fact]
    public async Task CreateAsync_InvalidTransactionProductNotFound_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var customer = new Customer
        {
            Id = customerId
        };
        var product = new Product
        {
            Id = productId
        };
        var transaction = new Transaction
        {
            CustomerId = customerId,
            ProductId = productId,
            Quantity = 5,
            Date = DateTime.UtcNow
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(customer);

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId));

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _transactionService.Create(transaction);
        });
    }
}
