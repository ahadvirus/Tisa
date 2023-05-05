using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NJection.LambdaConverter.Fluent;

namespace Tisa.Store.Web.Data.Converters;

public class TypeConvertor<T> : ValueConverter<T?, string>
{
    public TypeConvertor() : base(
        Lambda.TransformMethodTo<Func<T?, string>>()
            .From(() => ToProvider)
            .ToLambda(),
        Lambda.TransformMethodTo<Func<string, T?>>()
            .From(() => FromProvider)
            .ToLambda()
    )
    {
    }

    private static T? FromProvider(string value)
    {
        object? result = null;

        System.Type? type = System.Type.GetType(value);
        if (type != null)
        {
            result = System.Activator.CreateInstance(type);
        }

        return (T)result!;
    }

    private static string ToProvider(T? value)
    {
        string result = string.Empty;

        if (value != null)
        {
            string? fullName = value.GetType().FullName;

            if (!string.IsNullOrEmpty(fullName))
            {
                result = fullName;
            }
        }

        return result;
    }
}