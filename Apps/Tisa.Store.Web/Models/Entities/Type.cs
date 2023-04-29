using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Type
{
    public Type()
    {
        Name = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Attribute> Attributes { get; set; }
    public virtual ICollection<TypeValidator> Validators { get; set; }
}