using InvestmentPortfolio.Application.Pagination.Interface;

namespace InvestmentPortfolio.Application.Pagination;
public class PagedList<T> : IPagedList<T>
{
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviusPage => PageSize > 1;

    public PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static IPagedList<T> CreatePagedList(IQueryable<T> queryable, int page, int pageSize)
    {
        var totalCount = queryable.Count();
        var items = queryable.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}
