using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Products.Entities;

public class ObjectTypeDTO
{
    public dynamic Entry { get;}
    public string EntryType { get; }

    public string ValidType { get; }
    
    public ObjectTypeDTO(dynamic entry)
    {
        Entry = entry;
        PropertyInfo? property = typeof(Type).GetProperty(nameof(Type.FullName));
        
        string? entryType = null;
        string? validType = null;
        
        if (property != null)
        {
            entryType = (string?) property.GetValue(entry.GetType());
            validType = (string?) property.GetValue(typeof(JsonElement));
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

    public Task<IDictionary<string, object?>?> AccumulateData(IEnumerable<IAttributeDTO> attributes)
    {
        return Task.Run(() =>
        {
            IDictionary<string, object?>? result = null;

            if (IsValid)
            {
                result = new Dictionary<string, object?>();

                foreach (IAttributeDTO attribute in attributes)
                {
                    result.Add(attribute.Name, null);

                    if (attribute.GetType == null)
                    {
                        continue;
                    }

                    // Get default value of attribute
                    MethodInfo? @default = GetType().GetMethod(nameof(GetDefaultValue));

                    if (@default != null)
                    {
                        @default = @default.MakeGenericMethod(new Type[] { attribute.GetType });
                        result[attribute.Name] = @default.Invoke(this, null);
                    }

                    try
                    {
                        // Get value of attribute from json send by user
                        JsonElement value = ((JsonElement)Entry).GetProperty(JsonNamingPolicy.CamelCase.ConvertName(attribute.Name));

                        MethodInfo? method = typeof(JsonElement)
                            .GetMethods()
                            .Where(method => method.Name.Contains(attribute.Type))
                            .FirstOrDefault(method => method.ReturnType == attribute.GetType);
                        if (method != null)
                        {
                            result[attribute.Name] = method.Invoke(value, new object?[] { });
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(string.Format("\n{0}\n", e.Message));
                    }
                }
            }

            return result;
        });
    }

    public T? GetDefaultValue<T>()
    {
        return default(T);
    }
}