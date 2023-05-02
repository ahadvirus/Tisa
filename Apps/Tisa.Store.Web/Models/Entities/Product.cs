namespace Tisa.Store.Web.Models.Entities;

public class Product
{
    public Product()
    {
        Value = string.Empty;
    }
    
    public int Id { get; set; }
    public int EntityId { get; set; }
    public virtual Entity Entity { get; set; }
    public int AttributeEntityId { get; set; }
    public virtual AttributeEntity AttributeEntity { get; set; }
    public string Value { get; set; }
    public int Group { get; set; }
}