using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ValidateParameter : ParameterConvertor
{
    protected IAttributeDTO Attribute { get; }
    protected IEnumerable<IAttributeDTO> Attributes { get; }
    protected IValidatorParameter Validator { get; }

    

    public ValidateParameter(IAttributeDTO attribute, IEnumerable<IAttributeDTO> attributes,
        IValidatorParameter validator)
    {
        Attribute = attribute;
        Attributes = attributes;
        Validator = validator;
    }

    public async Task<bool> IsValid(string parameter)
    {
        bool result = false;
        if (Attribute.GetType != null)
        {
            result = await Validator.Validate(
                ConvertTo(parameter, Attribute),
                Attribute.GetType,
                ValidateAttribute
            );
        }

        return result;
    }
    
    private System.Type? ValidateAttribute(string entry)
    {
        System.Type? result = null;

        IAttributeDTO? attribute = Attributes.FirstOrDefault(attribute => attribute.Name == entry);

        if (attribute != null && !string.IsNullOrEmpty(attribute.Type))
        {
            result = System.Type.GetType(string.Format("{0}.{1}", nameof(System), attribute.Type));
        }

        return result;
    }
}