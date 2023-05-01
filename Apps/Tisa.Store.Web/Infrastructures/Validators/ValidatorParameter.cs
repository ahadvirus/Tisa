using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public abstract class ValidatorParameter : ValidatorAttribute, IValidatorParameter
{
    
    
    public abstract Task<bool> Validate(object? parameter, Type attribute, Func<string, Type?> attributeFinder);

}