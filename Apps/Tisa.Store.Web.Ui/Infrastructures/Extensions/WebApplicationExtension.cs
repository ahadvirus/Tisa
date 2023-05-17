using Microsoft.AspNetCore.Builder;
using Tisa.Store.Web.Ui.Infrastructures.Middlewares;

namespace Tisa.Store.Web.Ui.Infrastructures.Extensions;

public static class WebApplicationExtension
{
    public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
}