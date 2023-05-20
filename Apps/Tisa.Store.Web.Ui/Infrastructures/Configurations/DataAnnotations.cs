using System;
using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Ui.Infrastructures.Attributes;

namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

public class DataAnnotations
{
    /// <summary>
    /// Required attribute without part of attribute
    /// </summary>
    public string Required
    {
        get
        {
            return RemoveAttribute(nameof(RequiredAttribute));
        }
    }

    /// <summary>
    /// RegularExpression attribute without part of attribute
    /// </summary>
    public string RegularExpression
    {
        get
        {
            return RemoveAttribute(nameof(RegularExpressionAttribute));
        }
    }

    /// <summary>
    /// PascalName attribute without part of attribute
    /// </summary>
    public string PascalName
    {
        get
        {
            return RemoveAttribute(nameof(PascalNameAttribute));
        }
    }

    /// <summary>
    /// Remove <c>Attribute</c> word from string
    /// </summary>
    /// <param name="entry"><see cref="string"/> The parameter want to customize</param>
    /// <returns><see cref="string"/> Result after the remove</returns>
    protected string RemoveAttribute(string entry)
    {
        return entry.Replace(oldValue: nameof(Attribute), newValue: string.Empty);
    }
}