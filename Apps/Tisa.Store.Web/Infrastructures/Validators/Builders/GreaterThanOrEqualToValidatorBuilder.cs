using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators.Builders;

public class GreaterThanOrEqualToValidatorBuilder : ValidatorBuilder
{
    public override async Task<IPropertyValidator?> Build<T, TProperty>(object parameter)
    {
            IPropertyValidator? result = null;

            if ( parameter is string attribute && await ParameterIsAttribute(attribute))
            {
                parameter = BuildExpression<T, TProperty>(await GetAttributeName(attribute));
            }

            MethodInfo? method = typeof(FluentValidation.DefaultValidatorExtensions)
                .GetMethods()
                .Where(method =>
                    method.Name == nameof(FluentValidation.DefaultValidatorExtensions.GreaterThanOrEqualTo))
                .Where(method => method.GetParameters().Length == 2)
                .FirstOrDefault(method => method
                    .MakeGenericMethod(new Type[] { typeof(T), typeof(TProperty) })
                    .GetParameters()
                    .Any(methodParameter => methodParameter.ParameterType == parameter.GetType()));

            if (method != null)
            {
                method = method.MakeGenericMethod(new Type[] { typeof(T), typeof(TProperty) });
                result = new PropertyValidator(method, new object?[] { parameter });
            }

            return result;
    }
}