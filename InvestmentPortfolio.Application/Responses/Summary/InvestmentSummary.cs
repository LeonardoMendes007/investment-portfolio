namespace InvestmentPortfolio.Application.Responses.Summary;
public class InvestmentSummary
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal CurrentAmount { get; set; }
}
