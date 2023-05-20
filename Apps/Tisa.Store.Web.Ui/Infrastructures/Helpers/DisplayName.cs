using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;

namespace Tisa.Store.Web.Ui.Infrastructures.Helpers;

public class DisplayName<T, TResult> : RazorPage<IEnumerable<T>>
{
    protected Expression<Func<T, TResult>> Expression { get; }

    protected IStringLocalizer<T>? Localizer { get; }

    public DisplayName(
        Expression<Func<T, TResult>> expression,
        IStringLocalizer<T>? localizer
        )
    {
        Expression = expression;
        Localizer = localizer;
    }

    public override Task ExecuteAsync()
    {
        return Task.Run(() => WriteLiteral(value: Localizer != null ? Localizer[GetPropertyName()] : GetPropertyName()));
    }

    private string GetPropertyName()
    {
        return ((MemberExpression)Expression.Body).Member.Name;
    }
}