using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tisa.Store.Web.Ui.Infrastructures.Attributes;

/// <summary>
/// Validation attribute for validate the entry is start with capital character
/// </summary>
public class PascalNameAttribute : ValidationAttribute
{

    protected string Pattern
    {
        get
        {
            return "^[A-Z]$";
        }
    }

    public PascalNameAttribute() : base(errorMessage: "Please start {0} with capital character")
    {
    }

    public override bool IsValid(object? value)
    {
        return value != null ?
            (value is string ?
                new RegularExpressionAttribute(pattern: Pattern).IsValid(((string)value).First()) :
                false)
            : false;
    }
}