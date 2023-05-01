using System;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Validators.Checkers;

public class EmptyParameterValidator : ValidatorParameter
{
    public override Task<bool> Validate(object? parameter, Type attribute, Func<string, Type?> attributeFinder)
    {
        return Task.FromResult(parameter == null);
    }
}