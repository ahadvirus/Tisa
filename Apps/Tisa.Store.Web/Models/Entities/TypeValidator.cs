namespace Tisa.Store.Web.Models.Entities;

public class TypeValidator
{
    public int Id { get; set; }
    
    public virtual Type Type { get; set; }
    
    public int TypeId { get; set; }
    
    public virtual Validator Validator { get; set; }
    
    public int ValidatorId { get; set; }
}