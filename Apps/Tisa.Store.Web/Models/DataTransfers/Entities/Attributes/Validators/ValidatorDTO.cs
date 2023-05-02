using System.Collections.Generic;
using System.Linq;
using Tisa.Store.Web.Infrastructures.Contracts.Claims.Validators;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Models.DataTransfers.Entities.Attributes.Validators;

public class ValidatorDTO : IParameter
{
    public ValidatorDTO()
    {
        Validator = string.Empty;
        Description = string.Empty;
    }
    
    public int Id { get; set; }
    public string Description { get; set; }
    public IEnumerable<int> Types { get; set; }

    public IValidatorParameter? ParametersValidator
    {
        get
        {
            IValidatorParameter? result = null;

            if (!string.IsNullOrEmpty(Validator))
            {
                System.Type? type = System.Type.GetType(Validator);
                if (type != null && type.IsClass && !type.IsAbstract && type.GetInterfaces()
                        .Any(@interface => @interface == typeof(IValidatorParameter)))
                {
                    result = System.Activator.CreateInstance(type) as IValidatorParameter;
                }
            }
            
            return result;
        }
        set
        {
            throw new System.Exception(string.Empty);
        }
    }

    public string Validator { get; set; }
}