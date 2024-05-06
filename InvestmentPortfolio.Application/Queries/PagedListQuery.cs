using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MovieApi.Application.Queries;
public class PagedListQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public string GetKey()
    {
        return $"p:{Page}&ps:{PageSize}";
    }
}
