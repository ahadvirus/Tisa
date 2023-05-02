using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Contracts.Claims.Validators;

public interface IParameter
{
     IValidatorParameter? ParametersValidator { get; set; }
}