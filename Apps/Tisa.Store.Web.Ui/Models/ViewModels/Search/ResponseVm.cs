using System.Collections;
using System.Collections.Generic;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Search;

public record ResponseVm<T> : BaseVm, IEnumerable<T>
{
    public ResponseVm(IEnumerable<T> results)
    {
        Query = string.Empty;
        Results = results;
    }

    private IEnumerable<T> Results { get; init; }

    public IEnumerator<T> GetEnumerator()
    {
        return Results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}