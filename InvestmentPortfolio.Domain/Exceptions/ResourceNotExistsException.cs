namespace InvestmentPortfolio.Domain.Exceptions;
public class ResourceNotExistsException : Exception
{
    public Guid Id { get; set; }

    public ResourceNotExistsException(string message) : base(message)
    {
    }
    public ResourceNotExistsException(Guid id, string message) : base(message)
    {
        Id = id;
    }
}
