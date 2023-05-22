namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

/// <summary>
/// Class connect to api server 
/// </summary>
public record ApiOption
{
    public ApiOption()
    {
        Address = string.Empty;
    }

    /// <summary>
    /// The api address save init
    /// </summary>
    public string Address { get; init; }
}