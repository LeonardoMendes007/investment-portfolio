using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolio.API.Request.Transaction;

public class TransactionRequest
{
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
