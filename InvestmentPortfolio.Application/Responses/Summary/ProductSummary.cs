namespace InvestmentPortfolio.Application.Responses.Summary;
public class ProductSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
}
