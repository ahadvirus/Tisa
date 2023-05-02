using System.Collections.Generic;
using Tisa.Store.Web.Infrastructures.Contracts.Claims.Validators;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Models.ViewModels.Validators;

public class CreateVM : IParameter, IBuilder
{
    public string Name { get; set; }
    
    public string Description { get; set; }

    public IValidatorParameter ParametersValidator { get; set; }
    
    public IValidatorBuilder ValidatorBuilder { get; set; }
    
    public IEnumerable<string> Types { get; set; }
}