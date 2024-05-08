using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;

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
}
