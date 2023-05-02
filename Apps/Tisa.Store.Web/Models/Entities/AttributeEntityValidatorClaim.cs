namespace Tisa.Store.Web.Models.Entities;

public class AttributeEntityValidatorClaim
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public int AttributeEntityValidationId { get; set; }
    public virtual AttributeEntityValidator AttributeEntityValidation { get; set; }
}