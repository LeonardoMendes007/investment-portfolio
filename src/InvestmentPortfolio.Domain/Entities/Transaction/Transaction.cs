using InvestmentPortfolio.Domain.Entities.Base;

namespace InvestmentPortfolio.Domain.Entities.Transaction;
public class Transaction : Entity
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public TransactionType TransactionType { get; set; }
    public int Quantity { get; set; }
    public decimal PU { get; set; }
    public decimal Amount { get => Quantity * PU; }
    public DateTime Date { get; set; } = DateTime.Now;
    public Product.Product Product { get; set; }
    public Customer.Customer Customer { get; set; }

}
