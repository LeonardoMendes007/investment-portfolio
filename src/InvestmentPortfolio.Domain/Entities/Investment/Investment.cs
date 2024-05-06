namespace InvestmentPortfolio.Domain.Entities.Investment;
public class Investment
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal CurrentAmount { get => Product.CurrentPrice * Quantity; }
    public Product.Product Product { get; set; }
    public Customer.Customer Customer { get; set; }
}
