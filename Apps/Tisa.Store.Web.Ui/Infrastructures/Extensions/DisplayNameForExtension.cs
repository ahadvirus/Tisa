using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;
using Tisa.Store.Web.Ui.Infrastructures.Helpers;

namespace Tisa.Store.Web.Ui.Infrastructures.Extensions;

public static class DisplayNameForExtension
{
    private static IServiceProvider? ServiceProvider { get; set; }
    private static IPageRenderer? PageRenderer { get; set; }

    public static async Task<string> DisplayNameSearchFor<T, TProperty>(this IHtmlHelper<Models.ViewModels.Search.ResponseVm<T>> html,
        Expression<Func<T, TProperty>> expression)
    {
        return (PageRenderer != null && ServiceProvider != null) ?
           await PageRenderer.RenderPageAsync(
               page: new DisplayName<T, TProperty>(expression: expression, localizer: ServiceProvider.GetService<IStringLocalizer<T>>()),
               model: new T[] { }
               ) :
        ((MemberExpression)expression.Body).Member.Name;

    }


    public static void Provider(IPageRenderer? pageRenderer, IServiceProvider? serviceProvider)
    {
        PageRenderer = pageRenderer;
        ServiceProvider = serviceProvider;
    }

}