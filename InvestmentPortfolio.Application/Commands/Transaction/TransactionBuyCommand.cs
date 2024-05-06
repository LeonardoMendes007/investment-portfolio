using InvestmentPortfolio.Application.Commands.Transaction.Base;
using InvestmentPortfolio.Domain.Entities.Transaction;
using MediatR;

namespace InvestmentPortfolio.Application.Commands.Transaction;
public class TransactionBuyCommand : TransactionCommand, IRequest<Guid>
{
    public TransactionType transactionType { get; private set; } = TransactionType.Buy;
}
