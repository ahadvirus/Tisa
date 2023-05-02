using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Models.Entities;

public class Validator
{
    public Validator()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public virtual ICollection<TypeValidator> Types { get; set; }
    
    public virtual ICollection<ValidatorClaim> Claims { get; set; }

    public virtual ICollection<AttributeEntityValidator> Attributes { get; set; }
}