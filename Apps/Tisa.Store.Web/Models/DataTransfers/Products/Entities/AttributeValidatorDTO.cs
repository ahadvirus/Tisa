using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Models.DataTransfers.Products.Entities;

public class AttributeValidatorDTO : IAttributeValidatorDTO
{
    public string Name { get; set; }

    public IValidatorParameter? ParametersValidator
    {
        get
        {
            IValidatorParameter? result = null;

            if (!string.IsNullOrEmpty(Validator))
            {
                System.Type? type = System.Type.GetType(Validator);

                if (type != null)
                {
                    result = (IValidatorParameter?)System.Activator.CreateInstance(type);
                }
            }

            return result;
        }
        set { throw new System.Exception(string.Empty); }
    }

    public IValidatorBuilder? ValidatorBuilder
    {
        get
        {
            IValidatorBuilder? result = null;
            
            if (!string.IsNullOrEmpty(Builder))
            {
                System.Type? type = System.Type.GetType(Builder);

                if (type != null)
                {
                    result = (IValidatorBuilder?)System.Activator.CreateInstance(type);
                }
            }

            return result;
        }
        set { throw new System.Exception(string.Empty); }
    }

    public string Parameters { get; set; }
    public string Builder { get; set; }
    public string Validator { get; set; }
}