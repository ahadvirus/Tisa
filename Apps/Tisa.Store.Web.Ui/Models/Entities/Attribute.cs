using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Models.Entities;

public class Attribute : IEntity<int>
{
    public Attribute()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Translated name for attribute
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Translated description for attribute
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The attribute id in the api
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// The type id related to the attribute
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Type the related to the attribute
    /// </summary>
    public virtual Type Type { get; set; }
}