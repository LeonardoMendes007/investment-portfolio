namespace InvestmentPortfolio.Domain.Exceptions;
public class ProductExpiredException : Exception
{
    public Guid Id { get; set; }

    public ProductExpiredException(Guid id) : base($"Product with id = {id}, is expired.")
    {
        Id = id;
    }
}
