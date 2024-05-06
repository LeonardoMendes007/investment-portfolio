namespace InvestmentPortfolio.Application.Responses.Details;
public class InvestmentDetails
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public ProductDetails Product { get; set; }
}
