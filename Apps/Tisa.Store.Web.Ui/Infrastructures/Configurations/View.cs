namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

public abstract record View
{
    /// <summary>
    /// Default name for page title for make mistake down
    /// </summary>
    public string Title
    {
        get
        {
            return nameof(Title);
        }
    }

    /// <summary>
    /// Default action name for create new of entity in controller
    /// </summary>
    public string Create
    {
        get
        {
            return nameof(Create);
        }
    }
}