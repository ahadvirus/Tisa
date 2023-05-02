using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Infrastructures.Routes.Constraints;

public class AttributeEntityValidatorConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        bool result = false;

        if (httpContext != null)
        {
            ApplicationContext? context = httpContext.RequestServices.GetService<ApplicationContext>();
            if (context != null)
            {
                string headerName = nameof(Models.Entities.AttributeEntity) +
                                    nameof(Models.Entities.AttributeEntity.Id);

                if (httpContext.Request.Headers.ContainsKey(headerName))
                {
                    int attributeEntityId =
                        int.Parse(httpContext.Request.Headers[headerName].ToString());

                    if (values.TryGetValue(routeKey, out object? routeValue))
                    {
                        if (routeValue != null)
                        {
                            int attributeTypeId = context.AttributeEntities
                                .Where(attribute => attribute.Id == attributeEntityId)
                                .Select(attribute => attribute.Attribute.TypeId)
                                .FirstOrDefault();
                            if (attributeTypeId != 0)
                            {
                                int validatorId = context.Validators
                                    .Where(validator => validator.Name == (string)routeValue)
                                    .Where(validator => validator.Types.Any(type => type.TypeId == attributeTypeId))
                                    .Select(validator => validator.Id)
                                    .FirstOrDefault();
                                if (validatorId != 0)
                                {
                                    httpContext.Request.Headers.Add(
                                        nameof(Models.Entities.Validator) + nameof(Models.Entities.Validator.Id),
                                        validatorId.ToString()
                                    );

                                    int attributeEntityValidatorId = context.AttributeEntityValidators
                                        .Where(validator =>
                                            validator.AttributeEntityId == attributeEntityId &&
                                            validator.ValidatorId == validatorId)
                                        .Select(validator => validator.Id)
                                        .FirstOrDefault();
                                    if (attributeEntityValidatorId != 0)
                                    {
                                        httpContext.Request.Headers.Add(
                                            nameof(Models.Entities.AttributeEntityValidator) + nameof(Models.Entities.AttributeEntityValidator.Id),
                                            attributeEntityValidatorId.ToString()
                                        );
                                        result = true;
                                    }
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