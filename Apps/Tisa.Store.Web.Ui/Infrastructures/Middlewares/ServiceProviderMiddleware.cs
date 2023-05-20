using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Ui.Infrastructures.Middlewares;

public class ServiceProviderMiddleware
{
    private RequestDelegate Next { get; }

    public ServiceProviderMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        IServiceProvider provider = context.RequestServices;
        Type extensionNamespace = typeof(Extensions.DisplayNameForExtension);
        foreach (Type extension in GetType().Assembly.GetTypes()
                     .Where(type => type.IsClass && type.IsAbstract && type.IsSealed)
                     .Where(type => !string.IsNullOrEmpty(type.Namespace) && type.Namespace.Equals(extensionNamespace.Namespace)))
        {
            MethodInfo? method = extension.GetMethod(name: "Provider");

            if (method != null)
            {
                List<object?> parameters = new List<object?>();
                
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    if (parameter.ParameterType == typeof(IServiceProvider))
                    {
                        parameters.Add(provider);
                    }
                    else
                    {
                        object? service = provider.GetService(parameter.ParameterType);

                        if (service != null)
                        {
                            parameters.Add(service);
                        }
                    }
                }

                if (parameters.Count == method.GetParameters().Length)
                {
                    method.Invoke(obj: null, parameters: parameters.ToArray());
                }
                
            }
        }

        // Call the next delegate/middleware in the pipeline.
        await Next(context);
    }
}