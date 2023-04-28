using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Infrastructures.Routes.Constraints;

public class EntityConstraint : IRouteConstraint
{
    
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        ApplicationContext? context = null;

        if (httpContext != null)
        {
            context = httpContext.RequestServices.GetService<ApplicationContext>();


            if (context != null)
            {
                if (values.TryGetValue(routeKey, out object? routeValue))
                {
                    if (routeValue != null)
                    {
                        int entityId = context.Entities
                            .Where(entity => entity.Name == (string)routeValue)
                            .Select(entity => entity.Id)
                            .FirstOrDefault();
                        if (entityId != 0)
                        {
                            httpContext.Request.Headers.Add(
                                string.Format("{0}{1}", nameof(Models.Entities.Entity), nameof(Models.Entities.Entity.Id)),
                                entityId.ToString()
                                );
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}