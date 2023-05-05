using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.ViewModels.Products;

public class CreateVM : DynamicObject
{
    private IDictionary<string, object?> Properties { get; }

    public CreateVM(IEnumerable<IAttributeDTO> attributes, JsonElement entry)
    {
        Properties = new Dictionary<string, object?>();

        foreach (IAttributeDTO attribute in attributes)
        {
            Properties.Add(attribute.Name, null);

            if (attribute.GetType == null)
            {
                continue;
            }

            MethodInfo? @default = GetType().GetMethod(nameof(GetDefaultValue));

            if (@default != null)
            {
                @default = @default.MakeGenericMethod(new Type[] { attribute.GetType });
                Properties[attribute.Name] = @default.Invoke(this, null);
            }

            JsonElement value = entry.GetProperty(JsonNamingPolicy.CamelCase.ConvertName(attribute.Name));

            MethodInfo? method = typeof(JsonElement)
                .GetMethods()
                .Where(method => method.Name.Contains(attribute.Type))
                .FirstOrDefault(method => method.ReturnType == attribute.GetType);
            if (method != null)
            {
                Properties[attribute.Name] = method.Invoke(value, new object?[] { });
            }
        }
    }

    public T? GetDefaultValue<T>()
    {
        return default(T);
    }

    public IDictionary<string, object?> GetValues()
    {
        return Properties;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        return Properties.TryGetValue(binder.Name, out result);
    }
}