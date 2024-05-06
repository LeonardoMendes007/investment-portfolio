namespace InvestmentPortfolio.Application.Responses.Details;
public class ProductDetails
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public string Type { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
}
