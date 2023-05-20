namespace Tisa.Store.Web.Ui.Models.ViewModels.Search;

public record RequestVm
{
    public RequestVm()
    {
        Query = string.Empty;
    }

    public string Query { get; init; }
}