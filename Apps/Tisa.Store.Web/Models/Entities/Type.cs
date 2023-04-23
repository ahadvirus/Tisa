using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Type
{
    public Type()
    {
        Kind = string.Empty;
    }
    
    public int Id { get; set; }
    public string Kind { get; set; }
    public virtual ICollection<Attribute> Attributes { get; set; }
}