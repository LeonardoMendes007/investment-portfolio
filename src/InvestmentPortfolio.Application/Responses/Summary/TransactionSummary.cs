using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Responses.Summary;
public class TransactionSummary
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public TransactionType TransactionType { get; set; }
    public string TransactionTypeName { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

}
