using AutoMapper;
using InvestmentPortfolio.Application.Commands.Transaction;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.CommandHandlers;
public class TransactionCommandHandler : IRequestHandler<TransactionBuyCommand, Guid>,
                                            IRequestHandler<TransactionSellCommand, Guid>
{
    private readonly IInvestmentService _investmentService;
    private readonly ITransactionService _transactionService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        _transactionService = new TransactionService(unitOfWork);
        _investmentService = new InvestmentService(unitOfWork);
    }

    public async Task<Guid> Handle(TransactionBuyCommand request, CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(request);

        await _transactionService.Create(transaction);

        await _investmentService.BuyInvestment(transaction);
        
        await _unitOfWork.CommitAsync();

        return transaction.Id;
    }

    public async Task<Guid> Handle(TransactionSellCommand request, CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(request);

        await _transactionService.Create(transaction);

        await _investmentService.BuyInvestment(transaction);

        await _unitOfWork.CommitAsync();

        return transaction.Id;
    }
}
