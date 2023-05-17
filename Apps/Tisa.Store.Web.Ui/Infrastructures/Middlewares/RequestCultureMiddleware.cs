using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Ui.Infrastructures.Middlewares;

public class RequestCultureMiddleware
{
    private RequestDelegate Next { get; }
    private CultureInfo Culture { get; }

    public RequestCultureMiddleware(RequestDelegate next, IOptions<RequestLocalizationOptions> options)
    {
        Next = next;

        Culture = options.Value.DefaultRequestCulture.Culture;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        CultureInfo.CurrentCulture = Culture;
        CultureInfo.CurrentUICulture = Culture;
        // Call the next delegate/middleware in the pipeline.
        await Next(context);
    }
}