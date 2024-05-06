namespace InvestmentPortfolio.API.Request.Transaction;

public class TransactionRequest
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
