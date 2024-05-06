using AutoMapper;
using InvestmentPortfolio.Application.Commands.Transaction;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Transaction;
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

    public TransactionCommandHandler(IInvestmentService investmentService, ITransactionService transactionService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _investmentService = investmentService;
        _transactionService = transactionService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(TransactionBuyCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            throw new Exceptions.ValidationException("Quantity", $"'Quantity' cannot be less than 0.");
        }

        var transaction = _mapper.Map<Transaction>(request);

        await _transactionService.Create(transaction);

        await _investmentService.BuyInvestment(transaction);
        
        await _unitOfWork.CommitAsync();

        return transaction.Id;
    }

    public async Task<Guid> Handle(TransactionSellCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            throw new Exceptions.ValidationException("Quantity", $"'Quantity' cannot be less than 0.");
        }
        var transaction = _mapper.Map<Transaction>(request);

        await _transactionService.Create(transaction);

        await _investmentService.SellInvestment(transaction);

        await _unitOfWork.CommitAsync();

        return transaction.Id;
    }
}
