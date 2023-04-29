using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Tisa.Store.Web.Infrastructures.Contracts.Validator;

public interface IValidatorBuilder
{
    Task<IPropertyValidator?> Build<T, TProperty>(object parameter);
}