using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Contracts.Validator;

public interface IValidatorBuilder
{
    Task<IPropertyValidator?> Build<T, TProperty>(object parameter);
}