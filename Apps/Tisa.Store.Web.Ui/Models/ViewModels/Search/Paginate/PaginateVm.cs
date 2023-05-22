using System.Collections.Generic;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Search.Paginate;

public record ResponseVm<T> : Search.ResponseVm<T>
{
    public ResponseVm(IEnumerable<T> results) : base(results: results)
    {
        Query = string.Empty;
    }

    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    public int PageCount { get; init; }
}