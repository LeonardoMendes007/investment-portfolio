﻿using AutoMapper;
using InvestmentPortfolio.Application.Commands.Transaction;
using InvestmentPortfolio.Application.Handlers.CommandHandlers;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;
using System.Linq;

namespace InvestmentPortfolio.Application.Tests.Services;
public class CustomerServiceTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly ICustomerService _customerService;

    public CustomerServiceTest()
    {
        _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();

        _customerService = new CustomerService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task GetInvestmentByProductAsync_ValidId_ReturnsInvestment()
    {
        // Arrange
        Guid customerId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();
        var investmentAmount = 75;
        var quantity = 15;
        var currentPrice = 5;
        var currentAmount = currentPrice * quantity;

        var product = new Product
        {
            Id = productId,
            CurrentPrice = 5
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(customerId, productId))
            .ReturnsAsync(
                new Investment
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    InvestmentAmount = investmentAmount,
                    Quantity = quantity,
                    Product = product
                }
        );

        // Act
        var result = await _customerService.GetInvestmentByProductAsync(customerId, productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(investmentAmount, result.InvestmentAmount);
        Assert.Equal(currentAmount, result.CurrentAmount);
        Assert.Equal(quantity, result.Quantity);
    }

    [Fact]
    public async Task GetInvestmentByProductAsync_InvalidCustomerId_ThrowsResourceNotFoundException()
    {
        // Arrange
        Guid customerId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();
        
        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindInvestimentByProductIdAsync(customerId, productId));

        
        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _customerService.GetInvestmentByProductAsync(customerId, productId);
        });
    }

    [Fact]
    public async Task GetInvestmentByQueryAsync_ValidId_ReturnsIQueryableInvestment()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        var investments = new List<Investment>
        {
            new Investment
            {
                ProductId = Guid.NewGuid(),
                Product = new Product { Name = "Product 1" },
                Quantity = 15,
                InvestmentAmount = 500
            },
            new Investment
            {
                ProductId = Guid.NewGuid(),
                Product = new Product { Name = "Product 2" },
                Quantity = 10,
                InvestmentAmount = 300
            }
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindAllInvestments(customerId))
            .Returns(investments.AsQueryable());

        // Act
        var result = (await _customerService.GetInvestmentByQueryAsync(customerId)).ToList();

        // Assert
        Assert.NotNull(result);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(investments[i].ProductId, result[i].ProductId);
            Assert.Equal(investments[i].Product.Name, result[i].Product.Name);
            Assert.Equal(investments[i].Quantity, result[i].Quantity);
            Assert.Equal(investments[i].InvestmentAmount, result[i].InvestmentAmount);
            Assert.Equal(investments[i].CurrentAmount, result[i].CurrentAmount);
        }
    }

    [Fact]
    public async Task GetInvestmentByQueryAsync_InvalidCustomerId_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _customerService.GetInvestmentByQueryAsync(customerId);
        });
    }

    [Fact]
    public async Task GetTransactionsByProductAsync_ValidIds_ReturnsIQueryableTransaction()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CustomerId = customerId,
                Product = new Product { Name = "Product 1" },
                PU = 10,
                Quantity = 5,
                TransactionType = TransactionType.Buy,
                Date = DateTime.UtcNow.AddDays(10)
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CustomerId = customerId,
                Product = new Product { Name = "Product 2" },
                PU = 15,
                Quantity = 10,
                TransactionType = TransactionType.Sell, 
                Date = DateTime.UtcNow.AddDays(10)
            }
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(new Product());

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindTransactionsByProductId(customerId, productId))
            .Returns(transactions.AsQueryable());

        // Act
        var result = (await _customerService.GetTransactionsByProductAsync(customerId, productId)).ToList();

        // Assert
        Assert.NotNull(result);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(transactions[i].Id, result[i].Id);
            Assert.Equal(transactions[i].ProductId, result[i].ProductId);
            Assert.Equal(transactions[i].CustomerId, result[i].CustomerId);
            Assert.Equal(transactions[i].Product.Name, result[i].Product.Name);
            Assert.Equal(transactions[i].PU, result[i].PU);
            Assert.Equal(transactions[i].Quantity, result[i].Quantity);
            Assert.Equal(transactions[i].TransactionType, result[i].TransactionType);
            Assert.Equal(transactions[i].Date, result[i].Date);
        }
    }

    [Fact]
    public async Task GetTransactionsByProductAsync_InvalidCustomerId_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId));

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(new Product());

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _customerService.GetTransactionsByProductAsync(customerId, productId);
        });

    }

    [Fact]
    public async Task GetTransactionsByProductAsync_InvalidProductId_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId));
            
        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _customerService.GetTransactionsByProductAsync(customerId, productId);
        });

    }

    [Fact]
    public async Task GetTransactionByQueryAsync_ValidIds_ReturnsIQueryableTransaction()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CustomerId = customerId,
                Product = new Product { Name = "Product 1" },
                PU = 10,
                Quantity = 5,
                TransactionType = TransactionType.Buy,
                Date = DateTime.UtcNow.AddDays(10)
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CustomerId = customerId,
                Product = new Product { Name = "Product 2" },
                PU = 15,
                Quantity = 10,
                TransactionType = TransactionType.Sell,
                Date = DateTime.UtcNow.AddDays(10)
            }
        };

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindAllTransactions(customerId))
            .Returns(transactions.AsQueryable());

        // Act
        var result = (await _customerService.GetTransactionByQueryAsync(customerId)).ToList();

        // Assert
        Assert.NotNull(result);

        Assert.NotNull(result);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(transactions[i].Id, result[i].Id);
            Assert.Equal(transactions[i].ProductId, result[i].ProductId);
            Assert.Equal(transactions[i].CustomerId, result[i].CustomerId);
            Assert.Equal(transactions[i].Product.Name, result[i].Product.Name);
            Assert.Equal(transactions[i].Amount, result[i].Amount);
            Assert.Equal(transactions[i].Quantity, result[i].Quantity);
            Assert.Equal(transactions[i].TransactionType, result[i].TransactionType);
            Assert.Equal(transactions[i].Date, result[i].Date);
        }
    }

    [Fact]
    public async Task GetTransactionByQueryAsync_InvalidCustomerId_ThrowsResourceNotFoundException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        _mockUnitOfWork.Setup(x => x.CustomerRepository.FindByIdAsync(customerId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _customerService.GetTransactionByQueryAsync(customerId);
        });

    }

}
