using System.Threading.Tasks;
using FluentValidation.Validators;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public abstract class ValidatorBuilder : ValidatorAttribute, IValidatorBuilder
{
    public abstract Task<IPropertyValidator?> Build<T, TProperty>(object parameter);
}