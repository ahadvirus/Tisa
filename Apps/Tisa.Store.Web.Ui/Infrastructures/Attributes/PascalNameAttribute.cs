using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Tisa.Store.Web.Ui.Infrastructures.Attributes;

/// <summary>
/// Validation attribute for validate the entry is start with capital character
/// </summary>
[AttributeUsage(validOn: AttributeTargets.Property, AllowMultiple = false)]
public class PascalNameAttribute : RegularExpressionAttribute
{

    protected string Pattern
    {
        get
        {
            return "^[A-Z]$";
        }
    }

    public PascalNameAttribute() : base(pattern: "^[A-Z]$")
    {
    }

    public override bool IsValid(object? value)
    {
        value = value != null ? (value is string ? ((string)value).First() : string.Empty) : string.Empty;
        return base.IsValid(value: value);
    }
    /*
    public override string FormatErrorMessage(string name)
    {
        return string.Format(provider: CultureInfo.CurrentCulture, format: ErrorMessageString, args: new object?[] { name });
    }
    */
}