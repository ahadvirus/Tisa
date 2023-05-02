using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class AttributeEntityValidator
{
    public int Id { get; set; }
    public virtual AttributeEntity AttributeEntity { get; set; }
    public int AttributeEntityId { get; set; }
    public virtual Validator Validator { get; set; }
    public int ValidatorId { get; set; }
    public virtual ICollection<AttributeEntityValidatorClaim> Claims { get; set; }
    
}