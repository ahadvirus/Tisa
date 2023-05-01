namespace Tisa.Store.Web.Models.Entities;

public class TypeValidatorClaim
{
    public TypeValidatorClaim()
    {
        Key = string.Empty;
        Value = string.Empty;
    }
    
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public int TypeValidatorId { get; set; }
    
    public virtual TypeValidator TypeValidator { get; set; }
}