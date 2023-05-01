using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Validators.Checkers;

public class LengthParameterValidator : ValidatorParameter
{
    public override Task<bool> Validate(object? parameter, Type attribute, Func<string, Type?> attributeFinder)
    {
        return Task.Run(() =>
        {
            bool result = false;

            if (parameter != null && parameter.GetType().IsArray && ((Array)parameter).Length == 2)
            {
                result = ((object?[])parameter)
                    .Where(item => item != null)
                    .Count(item => item is int) == 2;
            }

            return result;
        });
    }
}