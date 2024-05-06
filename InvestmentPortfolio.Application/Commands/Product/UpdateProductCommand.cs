using MediatR;

namespace InvestmentPortfolio.Application.Commands.Product;
public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public Guid TypeId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}
