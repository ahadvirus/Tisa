using Microsoft.AspNetCore.Http;

namespace Tisa.Store.Web.Ui.Infrastructures.Extensions;

public static class HostNameExtension
{
    public static string GetHostName(this HttpContext context)
    {
        return string.Format(format: "{0}://{1}", args: new object[] { context.Request.Scheme, context.Request.Host });
    }
}