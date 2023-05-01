namespace Tisa.Store.Web.Models.Entities;

public class ValidatorClaim
{
    public ValidatorClaim()
    {
        Key = string.Empty;
        Value = string.Empty;
    }
    
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public int ValidatorId { get; set; }
    
    public virtual Validator Validator { get; set; }
}