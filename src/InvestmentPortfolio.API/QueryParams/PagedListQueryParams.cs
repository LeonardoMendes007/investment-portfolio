using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolio.API.QueryParams;

public class PagedListQueryParams
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    [Range(5, int.MaxValue)]
    public int PageSize { get; set; } = 30;

}
