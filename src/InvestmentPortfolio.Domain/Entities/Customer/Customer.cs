using InvestmentPortfolio.Domain.Entities.Base;
namespace InvestmentPortfolio.Domain.Entities.Customer;
public class Customer : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Balance { get; set; }
    public ICollection<Investment.Investment> Investments { get; set; }
    public ICollection<Transaction.Transaction> Transactions { get; set; }
}
