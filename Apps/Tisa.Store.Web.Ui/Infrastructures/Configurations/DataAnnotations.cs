using System;
using System.ComponentModel.DataAnnotations;

namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

public class DataAnnotations
{
    public string Required
    {
        get
        {
            return nameof(RequiredAttribute).Replace(oldValue: nameof(Attribute), newValue: string.Empty);
        }
    }
}