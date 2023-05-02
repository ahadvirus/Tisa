using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Infrastructures.Routes.Constraints;

public class AttributeEntityConstraint : EntitiesConstraint
{
    public override bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        bool result = false;

        if (httpContext != null)
        {
            string headerName = nameof(Models.Entities.Entity) + nameof(Models.Entities.Entity.Id);
            
            if (httpContext.Request.Headers.ContainsKey(headerName))
            {
                int entityId = int.Parse(httpContext.Request.Headers[headerName].ToString());
                ApplicationContext? context = httpContext.RequestServices.GetService<ApplicationContext>();
                
                if (context != null)
                {
                    if (values.TryGetValue(routeKey, out object? routeValue))
                    {
                        if (routeValue != null)
                        {
                            int attributeId = context.Attributes
                                .Where(attribute => attribute.Name == (string)routeValue)
                                .Select(attribute => attribute.Id)
                                .FirstOrDefault();
                            
                            if (attributeId != 0)
                            {
                                AppendToHeader(
                                    httpContext,
                                    nameof(Models.Entities.Attribute) + nameof(Models.Entities.Attribute.Id),
                                    attributeId.ToString()
                                );
                                
                                int attributeEntityId = context.AttributeEntities
                                    .Where(entity => entity.AttributeId == attributeId && entity.EntityId == entityId)
                                    .Select(entity => entity.Id)
                                    .FirstOrDefault();
                                
                                if (attributeEntityId != 0)
                                {
                                    AppendToHeader(
                                        httpContext,
                                        nameof(Models.Entities.AttributeEntity) + nameof(Models.Entities.AttributeEntity.Id),
                                        attributeEntityId.ToString()
                                    );
                                    
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
}