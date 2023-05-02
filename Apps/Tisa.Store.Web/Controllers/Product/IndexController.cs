using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Product;

[ApiController]
[Route(
    template: nameof(Models.Entities.Product) +
              "/{" +
              nameof(Models.ViewModels.Products.RequestVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}",
    Name = "[namespace].[controller]"
)]
public class IndexController : ControllerBase
{
    private ApplicationContext Context { get; }

    public IndexController(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<ActionResult> Invoke(
        [FromRoute] Models.ViewModels.Products.RequestVM request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<Models.DataTransfers.Products.ProductDTO> products = await Context.Products
            .Where(product => product.EntityId == request.EntityId)
            .Include(product => product.AttributeEntity)
            .ThenInclude(attribute => attribute.Attribute)
            .ThenInclude(attribute => attribute.Type)
            .GroupBy(product => product.Group)
            .Select(group => new Models.DataTransfers.Products.ProductDTO()
            {
                Name = group.Select(product => product.Entity.Name).First(),
                Properties = group.Select(product => new Models.DataTransfers.Products.PropertyDTO()
                {
                    Name = product.AttributeEntity.Attribute.Name,
                    Value = product.Value,
                    Type = product.AttributeEntity.Attribute.Type.Name
                })
            })
            //.ProjectTo<Models.DataTransfers.Products.ProductDTO>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        JsonNodeOptions options = new JsonNodeOptions()
        {
            PropertyNameCaseInsensitive = false
        };

        JsonArray result = new JsonArray(options);

        //List<JsonObject> result = new List<JsonObject>();

        foreach (Models.DataTransfers.Products.ProductDTO product in products)
        {
            JsonObject item = new JsonObject(options);
            foreach (Infrastructures.Contracts.DataTransfers.IPropertyDTO property in product.Properties)
            {
                System.Type? propertyType = System.Type.GetType(
                    string.Format(
                        "{0}.{1}",
                        nameof(System),
                        property.Type
                    )
                );
                if (propertyType != null)
                {
                    /*
                    object? value = System.Convert.ChangeType(property.Value, propertyType);
                    
                    
                    MethodInfo? jsonValueCreate = typeof(JsonValue)
                        .GetMethods()
                        .Where(method => method.Name == nameof(JsonValue.Create))
                        .Where(method =>
                            method.GetParameters().Count() == 2)
                        .Where(method => method.GetParameters().Any(parameter =>
                            value == null ? parameter.ParameterType.IsNullableType() : !parameter.ParameterType.IsNullableType()))
                        .FirstOrDefault(method => method.GetParameters().Any(parameter => value == null ? (
                            parameter.ParameterType.IsGenericType && parameter.ParameterType.GetGenericArguments()
                                .Any(generic => generic == propertyType)) : (parameter.ParameterType == propertyType)));

                    MethodInfo? jsonObjectAdd = typeof(JsonObject)
                        .GetMethods()
                        .Where(method =>  method.Name == nameof(JsonObject.Add))
                        .FirstOrDefault(method => method.GetParameters().Count() == 2);

                    if (jsonValueCreate != null && jsonObjectAdd != null)
                    {
                        
                        
                        value = jsonValueCreate.Invoke(null, new object?[]
                        {
                            value,
                            options
                        });
                        
                        jsonObjectAdd.Invoke(item, new object?[]
                        {
                            System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(property.Name),
                            value
                        });
                    }
                    
                    */
                    
                    MethodInfo? jsonValueCreate = typeof(JsonValue)
                        .GetMethods()
                        .Where(method => method.Name == nameof(JsonValue.Create))
                        .Where(method =>
                            method.GetParameters().Count() == 2)
                        .Where(method => method.GetParameters().Any(parameter =>
                            parameter.ParameterType.IsNullableType()))
                        .FirstOrDefault(method => method.GetParameters().Any(parameter =>  parameter.ParameterType.IsGenericType && parameter.ParameterType.GetGenericArguments()
                            .Any(generic => generic == propertyType)));

                    MethodInfo? jsonObjectAdd = typeof(JsonObject)
                        .GetMethods()
                        .Where(method =>  method.Name == nameof(JsonObject.Add))
                        .FirstOrDefault(method => method.GetParameters().Count() == 2);

                    if (jsonValueCreate != null && jsonObjectAdd != null)
                    {
                        jsonObjectAdd.Invoke(item, new object?[]
                        {
                            System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(property.Name),
                            jsonValueCreate.Invoke(null, new object?[]
                            {
                                System.Convert.ChangeType(property.Value, propertyType),
                                options
                            })
                        });
                    }
                    
                    /*
                    item.Add(
                        System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(property.Name), 
                        JsonValue.Create<string>(item)
                        );
                        */
                }
            }

            result.Add(item);
        }

        return Ok(result);
    }
}