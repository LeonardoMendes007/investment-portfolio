namespace InvestmentPortfolio.Application.Exceptions;
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; private set; }

    public ValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    public ValidationException(string propertyName, string errorMessage)
    {
        Errors = new Dictionary<string, string[]>
        {
            { propertyName, new[] { errorMessage } }
        };
    }
}
