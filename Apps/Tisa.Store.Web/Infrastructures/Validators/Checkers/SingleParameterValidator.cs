using System;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Validators.Checkers;

public class SingleParameterValidator : ValidatorParameter
{
    public override async Task<bool> Validate(object? parameter, Type attribute, Func<string, Type?> attributeFinder)
    {
        bool result = false;

        if (parameter != null)
        {
            if (parameter is string && !await ParameterIsAttribute((string)parameter))
            {
                result = parameter.GetType() == attribute;
            }

            if (parameter is string && await ParameterIsAttribute((string)parameter))
            {
                Type? anotherAttribute = attributeFinder(await GetAttributeName((string)parameter));
                if (anotherAttribute != null)
                {
                    result = attribute == anotherAttribute;
                }
            }
        }

        return result;
    }
}