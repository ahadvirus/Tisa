using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class AttributeEntity
{
    public int Id { get; set; }
    public int AttributeId { get; set; }
    public virtual Attribute Attribute { get; set; }
    public int EntityId { get; set; }
    public virtual Entity Entity { get; set; }

    public virtual ICollection<AttributeEntityValidator> Validators { get; set; }
    public virtual ICollection<Product> Values { get; set; }
}