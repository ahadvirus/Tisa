using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Tisa.Store.Web.Infrastructures.Validators.Builders;

public class EqualValidatorBuilder : ValidatorBuilder
{
    public override Task<IPropertyValidator?> Build<T, TProperty>(object parameter)
    {
        throw new System.NotImplementedException();
    }
}