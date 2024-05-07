using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Responses.Details;
public class TransactionDetails
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public string ProductName { get; set; }
    public TransactionType TransactionType { get; set; }
    public string TransactionTypeName { get; set; }
    public decimal PU { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get => Quantity * PU; }
    public DateTime Date { get; set; } = DateTime.Now;
}
