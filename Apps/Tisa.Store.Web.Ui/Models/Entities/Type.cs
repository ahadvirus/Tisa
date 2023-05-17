using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Models.Entities;

/// <summary>
/// To translate type entity to any language
/// </summary>
public class Type : IEntity<int>
{
    public Type()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }
    /// <summary>
    /// Translate name for type
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Save Type Id from the Api
    /// </summary>
    public int TypeId { get; set; }

}