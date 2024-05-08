using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;

namespace InvestmentPortfolio.Application.Tests.Services;
public class InvestmentServiceTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly IInvestmentService _investmentService;

    public InvestmentServiceTest()
    {
        _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();

        _investmentService = new InvestmentService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task BuyInvestmentAsync_ValidTransaction_WithoutPastInvestment_ReturnsInvestment()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CurrentPrice = 5,
            ExpirationDate = DateTime.Now.AddDays(30),
            IsActive = true
        };

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Balance = 1000 
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            CustomerId = customer.Id,
            Product = product,
            Customer = customer,
            Quantity = 5,
            PU = product.CurrentPrice,
            Date = DateTime.UtcNow
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId));

        _mockUnitOfWork.Setup(x => x.InvestmentRepository.SaveAsync(It.IsAny<Investment>()));

        // Act
        var result = await _investmentService.BuyInvestmentAsync(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.ProductId, result.Product.Id);
        Assert.Equal(transaction.CustomerId, result.Customer.Id);
        Assert.Equal(transaction.Quantity, result.Quantity);
        Assert.Equal(transaction.Quantity * transaction.PU, result.InvestmentAmount);
    }

    [Fact]
    public async Task BuyInvestmentAsync_ValidTransaction_WithPastInvestment_ReturnsInvestment()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CurrentPrice = 5,
            ExpirationDate = DateTime.Now.AddDays(30),
            IsActive = true
        };

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Balance = 1000
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            CustomerId = customer.Id,
            Product = product,
            Customer = customer,
            Quantity = 5,
            PU = product.CurrentPrice,
            Date = DateTime.UtcNow
        };

        var investment = new Investment
        {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Customer = customer,
            Product = product,
            InvestmentAmount = 50,
            Quantity = 10
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId))
            .ReturnsAsync(investment);

        _mockUnitOfWork.Setup(x => x.InvestmentRepository.SaveAsync(It.IsAny<Investment>()));

        // Act
        var result = await _investmentService.BuyInvestmentAsync(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.ProductId, result.Product.Id);
        Assert.Equal(transaction.CustomerId, result.Customer.Id);
        Assert.Equal(investment.Quantity, result.Quantity);
        Assert.Equal(investment.Quantity * transaction.PU, result.InvestmentAmount);
    }

    [Fact]
    public async Task BuyInvestmentAsync_ProductInactive_ThrowsProductIsInativeException()
    {
        // Arrange
        var product = new Product
        {
            IsActive = false
        };

        var transaction = new Transaction
        {
            ProductId = product.Id,
            Product = product
        };

        // Assert
        await Assert.ThrowsAsync<ProductIsInativeException>(async () =>
        {
            // Act
            await _investmentService.BuyInvestmentAsync(transaction);
        });
    }

    [Fact]
    public async Task BuyInvestmentAsync_ProductExpired_ThrowsProductIsInativeException()
    {
        // Arrange
        var product = new Product
        {
            IsActive = true,
            ExpirationDate = DateTime.Now.AddDays(-2)
        };

        var transaction = new Transaction
        {
            ProductId = product.Id,
            Product = product
        };

        // Assert
        await Assert.ThrowsAsync<ProductIsInativeException>(async () =>
        {
            // Act
            await _investmentService.BuyInvestmentAsync(transaction);
        });
    }

    [Fact]
    public async Task SellInvestmentAsync_ValidTransaction_ReturnsInvestment()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CurrentPrice = 5,
            ExpirationDate = DateTime.Now.AddDays(30),
            IsActive = true
        };

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Balance = 1000
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            CustomerId = customer.Id,
            Product = product,
            Customer = customer,
            Quantity = 5,
            PU = product.CurrentPrice,
            Date = DateTime.UtcNow
        };

        var investment = new Investment
        {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Customer = customer,
            Product = product,
            InvestmentAmount = 50,
            Quantity = 10
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId))
            .ReturnsAsync(new Investment
            {
                CustomerId = customer.Id,
                ProductId = product.Id,
                Customer = customer,
                Product = product,
                InvestmentAmount = 50,
                Quantity = 10
            });

        // Act
        var result = await _investmentService.SellInvestmentAsync(transaction);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(investment.ProductId, result.Product.Id);
        Assert.Equal(investment.CustomerId, result.Customer.Id);
        Assert.Equal(investment.Quantity - transaction.Quantity, result.Quantity);
        Assert.Equal((investment.Quantity - transaction.Quantity) * transaction.PU, result.InvestmentAmount);
    }

    [Fact]
    public async Task SellInvestmentAsync_InvestmentNotExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CurrentPrice = 5
        };

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Balance = 1000
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            CustomerId = customer.Id,
            Product = product,
            Customer = customer,
            Quantity = 5,
            PU = product.CurrentPrice,
            Date = DateTime.UtcNow
        };

        var investment = new Investment
        {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Customer = customer,
            Product = product,
            InvestmentAmount = 50,
            Quantity = 10
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId));

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            // Act
            await _investmentService.SellInvestmentAsync(transaction);
        });
    }

    [Fact]
    public async Task SellInvestmentAsync_QuantitySoldIsGreaterThanBalance_ThrowsInvalidOperationException()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CurrentPrice = 5
        };

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Balance = 1000
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            CustomerId = customer.Id,
            Product = product,
            Customer = customer,
            Quantity = 5,
            PU = product.CurrentPrice,
            Date = DateTime.UtcNow
        };

        var investment = new Investment
        {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Customer = customer,
            Product = product,
            InvestmentAmount = 10,
            Quantity = 2
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId))
            .ReturnsAsync(investment);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            // Act
            await _investmentService.SellInvestmentAsync(transaction);
        });
    }
}
