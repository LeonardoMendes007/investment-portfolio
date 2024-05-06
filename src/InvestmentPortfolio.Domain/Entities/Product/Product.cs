using InvestmentPortfolio.Domain.Entities.Base;

namespace InvestmentPortfolio.Domain.Entities.Product;
public class Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Transaction.Transaction> Transactions { get; set; }
    public ICollection<Investment.Investment> Investments { get; set; }
}
