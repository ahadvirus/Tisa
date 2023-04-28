using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Validator
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Title { get; set; }
    
    public virtual ICollection<TypeValidator> Types { get; set; }
}