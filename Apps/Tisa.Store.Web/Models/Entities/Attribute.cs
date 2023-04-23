namespace Tisa.Store.Web.Models.Entities;

public class Attribute
{
    public Attribute()
    {
        Name = string.Empty;
        Title = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public int TypeId { get; set; }
    public virtual Type Type { get; set; }
}