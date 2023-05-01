using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ValidatorAttribute
{

    private string Pattern
    {
        get
        {
            return "Attribute\\.(\\w+)";
        }
    }

    private RegexOptions Options
    {
        get
        {
            return (RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
    }
    
    
    protected Task<bool> ParameterIsAttribute(string parameter)
    {
        return Task.FromResult(Regex.IsMatch(input: parameter, pattern: Pattern, options: Options));
    }

    protected async Task<string> GetAttributeName(string parameter)
    {
        return 
            await ParameterIsAttribute(parameter) ? 
                Regex.Matches(input: parameter, pattern: Pattern, options: Options).Select(match => match.Value).First() : 
                string.Empty;
    }
}