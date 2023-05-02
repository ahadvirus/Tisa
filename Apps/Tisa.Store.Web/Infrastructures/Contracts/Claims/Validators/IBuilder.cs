using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Contracts.Claims.Validators;

public interface IBuilder
{
    IValidatorBuilder? ValidatorBuilder { get; set; }
}