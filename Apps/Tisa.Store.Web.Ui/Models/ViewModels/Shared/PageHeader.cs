namespace Tisa.Store.Web.Ui.Models.ViewModels.Shared;

public record PageHeader
{
    public PageHeader()
    {
        Title = string.Empty;
        CreateButtonTitle = string.Empty;
        CreateButtonLink = string.Empty;
        HaveCreateButton = false;
    }

    public object? Title { get; init; }
    public bool HaveCreateButton { get; init; }
    public object? CreateButtonTitle { get; init; }
    public string CreateButtonLink { get; init;}
}