using System.Collections.Generic;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Models.ViewModels.Validators;

public class CreateVM
{
    public string Name { get; set; }
    
    public string Description { get; set; }

    public bool NeedParameters { get; set; }

    public IValidatorParameter ParametersValidator { get; set; }
    
    public IValidatorBuilder ValidatorBuilder { get; set; }
    
    public IEnumerable<string> Types { get; set; }
}