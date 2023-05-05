using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ValidatorRule<T> : ParameterConvertor
{
    private IAttributeDTO Attribute { get; }

    private IEnumerable<IAttributeDTO> Attributes { get; }

    private IAttributeValidatorDTO Validator { get; set; }

    public ValidatorRule(
        IAttributeDTO attribute,
        IEnumerable<IAttributeDTO> attributes,
        IAttributeValidatorDTO validator
    )
    {
        Attribute = attribute;
        Attributes = attributes;
        Validator = validator;
    }

    public async Task<IPropertyValidator?> GetRule()
    {
        IPropertyValidator? result = null;

        if (Validator.ParametersValidator != null)
        {
            ValidateParameter parameter = new ValidateParameter(Attribute, Attributes, Validator.ParametersValidator);

            if (await parameter.IsValid(Validator.Parameters) && Validator.ValidatorBuilder != null)
            {
                MethodInfo? build = Validator.ValidatorBuilder.GetType()
                    .GetMethod(nameof(Validator.ValidatorBuilder.Build));

                if (build != null)
                {
                    System.Type instanceType = typeof(T);
                    System.Type? propertyType = Attribute.GetType;
                    if (propertyType != null)
                    {
                        build = build.MakeGenericMethod(new System.Type[] { instanceType, propertyType });

                        object? invoke = build.Invoke(Validator.ValidatorBuilder,
                            new object?[] { await ConvertTo(Validator.Parameters, Attribute) });
                        if (invoke != null && invoke.GetType() == typeof(Task<IPropertyValidator?>))
                        {
                            result = await (Task<IPropertyValidator?>)invoke;
                        }
                    }
                }
            }
        }

        return result;
    }
}