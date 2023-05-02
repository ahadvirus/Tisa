using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Tisa.Store.Web.Infrastructures.Routes.Constraints;

public abstract class EntitiesConstraint : IRouteConstraint
{
    public abstract bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection);

    protected void AppendToHeader(HttpContext context, string key, string value)
    {
        if (context.Request.Headers.ContainsKey(key))
        {
            context.Request.Headers[key] = value;
        }
        else
        {
            context.Request.Headers.Add(key, value);
        }
    }
}