using System.Collections.Generic;
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

    public bool NeedParameters { get; set; }

    public IValidatorParameter ParametersValidator { get; set; }
    
    public IValidatorBuilder ValidatorBuilder { get; set; }
    
    public virtual ICollection<TypeValidator> Types { get; set; }
}