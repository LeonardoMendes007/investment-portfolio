namespace InvestmentPortfolio.API.Request;

public class UpdateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}
