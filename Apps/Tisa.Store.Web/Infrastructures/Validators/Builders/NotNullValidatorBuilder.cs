using System.Reflection;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators.Builders;

public class NotNullValidatorBuilder : ValidatorBuilder
{
    public override Task<IPropertyValidator?> Build<T, TProperty>(object parameter)
    {
        return Task.Run(() =>
        {
            IPropertyValidator? result = null;

            MethodInfo? method = typeof(FluentValidation.DefaultValidatorExtensions)
                .GetMethod(nameof(FluentValidation.DefaultValidatorExtensions.NotNull));

            if (method != null)
            {
                method = method.MakeGenericMethod(new System.Type[] { typeof(T), typeof(TProperty) });
                result = new PropertyValidator(method);
            }
            
            return result;
        });
    }
}