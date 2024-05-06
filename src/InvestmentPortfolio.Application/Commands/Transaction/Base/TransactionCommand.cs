using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Commands.Transaction.Base;
public abstract class TransactionCommand
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
