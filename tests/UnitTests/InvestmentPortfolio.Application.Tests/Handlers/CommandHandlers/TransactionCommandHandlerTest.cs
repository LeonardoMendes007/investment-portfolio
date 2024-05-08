using AutoMapper;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Commands.Transaction;
using InvestmentPortfolio.Application.Handlers.CommandHandlers;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Validators.Product;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPortfolio.Application.Tests.Handlers.CommandHandlers;
public class TransactionCommandHandlerTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<ITransactionService> _mockTransactionService;
    private readonly Mock<IInvestmentService> _mockInvestmentService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    private readonly TransactionCommandHandler transactionCommandHandler;

    public TransactionCommandHandlerTest()
    {
        _mockTransactionService = _mockRepository.Create<ITransactionService>();
        _mockInvestmentService = _mockRepository.Create<IInvestmentService>();
        _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();
        _mockMapper = _mockRepository.Create<IMapper>();

        transactionCommandHandler = new TransactionCommandHandler(_mockInvestmentService.Object, _mockTransactionService.Object, _mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_CreateTransactionBuyCommand_ReturnsGuid()
    {
        // Arrange
        var transactionBuyCommand = new TransactionBuyCommand
        {
            CustomerId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 5
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CustomerId = transactionBuyCommand.CustomerId,
            ProductId = transactionBuyCommand.CustomerId,
            Quantity = transactionBuyCommand.Quantity
        };

        _mockMapper
            .Setup(mapper => mapper.Map<Transaction>(transactionBuyCommand))
            .Returns(transaction);

        _mockTransactionService
            .Setup(service => service.Create(It.IsAny<Transaction>()))
            .ReturnsAsync(transaction);

        _mockInvestmentService
            .Setup(service => service.BuyInvestmentAsync(It.IsAny<Transaction>()));

        // Act
        var actual = await transactionCommandHandler.Handle(transactionBuyCommand, CancellationToken.None);

        //Assert
        Assert.Equal(transaction.Id, actual);
    }

    [Fact]
    public async Task Handle_CreateTransactionSellCommand_ReturnsGuid()
    {
        // Arrange
        var transactionSellCommand = new TransactionSellCommand
        {
            CustomerId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 5
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CustomerId = transactionSellCommand.CustomerId,
            ProductId = transactionSellCommand.CustomerId,
            Quantity = transactionSellCommand.Quantity
        };

        _mockMapper
            .Setup(mapper => mapper.Map<Transaction>(transactionSellCommand))
            .Returns(transaction);

        _mockTransactionService
            .Setup(service => service.Create(It.IsAny<Transaction>()))
            .ReturnsAsync(transaction);

        _mockInvestmentService
            .Setup(service => service.BuyInvestmentAsync(It.IsAny<Transaction>()));

        // Act
        var actual = await transactionCommandHandler.Handle(transactionSellCommand, CancellationToken.None);

        //Assert
        Assert.Equal(transaction.Id, actual);
    }
}
