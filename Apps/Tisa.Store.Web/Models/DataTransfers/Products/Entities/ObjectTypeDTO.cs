using System;

namespace Tisa.Store.Web.Models.DataTransfers.Products.Entities;

public class ObjectTypeDTO
{
    public string EntryType { get; }

    public string ValidType { get; }
    
    public ObjectTypeDTO(dynamic entry)
    {
        System.Reflection.PropertyInfo? property = typeof(System.Type).GetProperty(nameof(System.Type.FullName));
        
        string? entryType = null;
        string? validType = null;
        
        if (property != null)
        {
            entryType = (string?) property.GetValue(entry.GetType());
            validType = (string?) property.GetValue(typeof(System.Text.Json.JsonElement));
        }

        EntryType = string.IsNullOrEmpty(entryType) ? string.Empty : entryType;
        ValidType = string.IsNullOrEmpty(validType) ? string.Empty : validType;
    }

    public bool IsValid
    {
        get
        {
            return string.Compare(EntryType, ValidType, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}