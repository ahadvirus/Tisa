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

            return result;
        }
        set { throw new System.Exception(string.Empty); }
    }

    public IValidatorBuilder? ValidatorBuilder
    {
        get
        {
            IValidatorBuilder? result = null;

            return result;
        }
        set { throw new System.Exception(string.Empty); }
    }

    public string Parameters { get; set; }
    public string Builder { get; set; }
    public string Validator { get; set; }
}