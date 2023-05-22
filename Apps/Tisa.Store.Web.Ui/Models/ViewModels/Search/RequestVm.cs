namespace Tisa.Store.Web.Ui.Models.ViewModels.Search;

public record RequestVm : BaseVm
{
    public RequestVm()
    {
        Query = string.Empty;
    }
}