using System.Linq;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ParameterConvertor : ValidatorAttribute
{
    private char Separator
    {
        get { return '|'; }
    }

    protected async Task<object?> ConvertTo(string entry, IAttributeDTO attribute)
    {
        return (
            entry.Contains(Separator)
                ? entry.Split(Separator)
                    .Where(item => !string.IsNullOrEmpty(item))
                    .Select(item => ChangeType(item, attribute))
                : (
                    !string.IsNullOrEmpty(entry)
                        ? (
                            await ParameterIsAttribute(entry)
                                ? entry
                                : ChangeType(entry, attribute)
                        )
                        : null
                )
        );
    }


    private async Task<object?> ChangeType(string item, IAttributeDTO attribute)
    {
        object? result = null;
        if (attribute.GetType != null)
        {
            try
            {
                result = !await ParameterIsAttribute(item)
                    ? System.Convert.ChangeType(item, attribute.GetType)
                    : item;
            }
            catch (System.Exception)
            {
                result = null;
            }
        }

        return result;
    }
}