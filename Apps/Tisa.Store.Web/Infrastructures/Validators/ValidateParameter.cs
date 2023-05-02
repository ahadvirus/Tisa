using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ValidateParameter : ValidatorAttribute
{
    protected IAttributeDTO Attribute { get; }
    protected IEnumerable<IAttributeDTO> Attributes { get; }
    protected IValidatorParameter Validator { get; }

    protected char Seperator
    {
        get { return '|'; }
    }

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
        System.Type? attributeType = System.Type.GetType(string.Format("{0}.{1}", nameof(System), Attribute.Type));
        if (attributeType != null)
        {
            result = await Validator.Validate(
                (
                    parameter.Contains(Seperator)
                        ? parameter.Split(Seperator)
                            .Where(item => !string.IsNullOrEmpty(item))
                            .Select(Convert)
                        : (
                            !string.IsNullOrEmpty(parameter)
                                ? (
                                    await ParameterIsAttribute(parameter)
                                        ? parameter
                                        : System.Convert.ChangeType(parameter, attributeType)
                                )
                                : null
                        )
                ),
                attributeType,
                ValidateAttribute
            );
        }

        return result;
    }

    private async  Task<object?> Convert(string item)
    {
        object? result = null;
        System.Type? attributeType = System.Type.GetType(string.Format("{0}.{1}", nameof(System), Attribute.Type));
        if (attributeType != null)
        {
            try
            {
                result = !await ParameterIsAttribute(item)
                    ? System.Convert.ChangeType(item, attributeType)
                    : item;
            }
            catch (System.Exception)
            {
                result = null;
            }
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