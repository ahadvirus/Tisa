using System;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Validators.Checkers;

public class MathematicsParameterValidator : ValidatorParameter
{
    public override async Task<bool> Validate(object? parameter, Type attribute, Func<string, Type?> attributeFinder)
    {
        bool result = false;

        if (parameter != null)
        {

            if (parameter is string)
            {
                if (await ParameterIsAttribute((string)parameter))
                {
                    Type? anotherAttribute = attributeFinder(await GetAttributeName((string)parameter));
                    if (anotherAttribute != null)
                    {
                        result = attribute == anotherAttribute;
                    }
                }
            }
            else
            {
                foreach (Type? type in new Type?[] { Type.GetType(nameof(Int32)), Type.GetType(nameof(Single)) })
                {
                    if (type != null && parameter.GetType() == type)
                    {
                        result = attribute == type;
                        break;
                    }
                }
            }
        }

        return result;
    }
}