using System.ComponentModel.DataAnnotations;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Search;

public abstract record BaseVm
{
    protected BaseVm()
    {
        Query = string.Empty;
    }

    [Display(Name = nameof(Query))]
    public string Query { get; init; }
}