using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class PropertyValidator : IPropertyValidator
{
    public PropertyValidator(MethodInfo method, object?[]? parameters = null)
    {
        Method = method;
        Parameters = parameters;
    }

    public MethodInfo Method { get; }
    public object?[]? Parameters { get; }
    
    public Task Add(object rule)
    {
        return Task.Run(() =>
        {
            List<object?> parameters = new List<object?> { rule };

            if (Parameters != null)
            {
                foreach (object? item in Parameters)
                {
                    parameters.Add(item);
                }
            }

            Method.Invoke(null, parameters.ToArray());
        });
    }
}