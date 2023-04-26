using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tisa.Store.Web.Infrastructures.Attributes;
using Tisa.Store.Web.Infrastructures.Contracts;

namespace Tisa.Store.Web.Infrastructures.Extensions;

public static class ApplySeed
{
    public static async void UseApplySeedFromAssembly(this WebApplication app, Assembly assembly)
    {
        await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
        {


            IDictionary<Type, DbContext?> contexts = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(DbContext)))
                .ToDictionary(type => type, type => (DbContext?)scope.ServiceProvider.GetService(type));

            foreach (Type context in contexts.Where(pair => pair.Value != null).Select(pair => pair.Key))
            {
                object? result = null;
                bool find = false;

                if (!find)
                {
                    ConstructorInfo[] constructors = context.GetConstructors();

                    if (constructors.Length == 0)
                    {
                        find = true;
                        result = Activator.CreateInstance(context);
                    }

                    if (!find)
                    {
                        result = constructors.Where(constructor => constructor.GetParameters().Length == 0)
                            .Select(constructor => constructor.Invoke(new object[] { }))
                            .FirstOrDefault();

                        find = result != null;
                    }
                }

                if (find && result != null)
                {
                    contexts[context] = (DbContext)result;
                }
            }

            IEnumerable<Type> seeds = assembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract &&
                               type.GetInterfaces().Any(@interface =>
                                   @interface.IsGenericType &&
                                   @interface.GetGenericTypeDefinition() == typeof(ISeed<>)
                               ))
                .Where(type =>
                    type.GetCustomAttributes().Any(attribute => attribute.GetType() == typeof(OrderAttribute)))
                .OrderBy(type => (type.GetCustomAttribute(typeof(OrderAttribute)) as OrderAttribute)?.Number);
            foreach (Type seed in seeds)
            {
                object? instance = null;

                Type? context = seed.GetInterfaces()
                    .Where(type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ISeed<>))
                    .Select(type => type.GetGenericArguments().FirstOrDefault())
                    .FirstOrDefault();

                if (context != null && contexts.ContainsKey(context) && contexts[context] != null)
                {
                    ConstructorInfo[] constructors = seed.GetConstructors();

                    if (constructors.Length == 0)
                    {
                        instance = Activator.CreateInstance(seed);
                    }
                    else
                    {
                        foreach (ConstructorInfo constructor in constructors)
                        {
                            List<object> parameters = new List<object>();

                            foreach (object? service in constructor.GetParameters()
                                         .Select(parameter => scope.ServiceProvider.GetService(parameter.ParameterType)))
                            {
                                if (service != null)
                                {
                                    parameters.Add(service);
                                }
                            }

                            if (parameters.Count == constructor.GetParameters().Length)
                            {
                                instance = constructor.Invoke(parameters.ToArray());
                                break;
                            }
                        }
                    }

                    if (instance != null)
                    {
                        MethodInfo? method = instance.GetType().GetMethod(nameof(ISeed<DbContext>.Invoke));
                        if (method != null)
                        {
                            await (Task)method.Invoke(instance, new object?[] { contexts[context] })!;
                        }
                    }
                }
            }
        }
    }
}