namespace Tisa.Store.Web.Ui.Models.ViewModels.Search.Paginate;

public record RequestVm : Search.RequestVm
{
    public RequestVm()
    {
        Query = string.Empty;
    }

    public int Page { get; init; }
    public int Count { get; init; }
}