namespace InvestmentPortfolio.API.Request;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}
