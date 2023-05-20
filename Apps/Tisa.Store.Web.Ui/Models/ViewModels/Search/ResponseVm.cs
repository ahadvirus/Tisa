using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Search;

public record ResponseVm<T> : IEnumerable<T>
{
    public ResponseVm(IEnumerable<T> results)
    {
        Query = string.Empty;
        Results = results;
    }

    [Display]
    public string Query { get; init; }

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