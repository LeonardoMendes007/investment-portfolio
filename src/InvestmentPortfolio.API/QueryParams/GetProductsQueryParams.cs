namespace InvestmentPortfolio.API.QueryParams;

public class GetProductsQueryParams : PagedListQueryParams
{
    public bool Inactive { get; set; } = false;
}
