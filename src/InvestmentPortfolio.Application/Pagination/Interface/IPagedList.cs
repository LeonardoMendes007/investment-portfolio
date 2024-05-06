namespace InvestmentPortfolio.Application.Pagination.Interface;
public interface IPagedList<T>
{
    List<T> Items { get; }
    int Page { get; }
    int PageSize { get; }
    int TotalCount { get; }
    bool HasNextPage { get; }
    bool HasPreviusPage { get; }
}
