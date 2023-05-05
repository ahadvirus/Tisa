using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class ValidatorFactory
{
    private Type? BaseType { get; }
    
    private IServiceProvider Provider { get; }

    public ValidatorFactory(Type type, IServiceProvider provider)
    {
        Provider = provider;
        BaseType = null;

        if (type.GetInterfaces()
            .Any(@interface => @interface == typeof(Contracts.Validator.IValidator)))
        {
            BaseType = type;
        }
    }

    public Contracts.Validator.IValidator? CreateInstance(Type entityType, IEnumerable<IAttributeEntityDTO> attributeValidator)
    {
        Contracts.Validator.IValidator? result = null;

        if (BaseType != null)
        {
            Type baseType = BaseType.IsGenericType ? BaseType.MakeGenericType(new Type[] { entityType }) : BaseType;
            
            if (baseType.GetConstructors().Length == 0)
            {
                result = (Contracts.Validator.IValidator?)Activator.CreateInstance(BaseType);
            }
            else
            {
                List<object> parameters = new List<object>()
                {
                    attributeValidator
                };
                
                foreach (ConstructorInfo constructor in baseType.GetConstructors())
                {
                    foreach (ParameterInfo parameter in constructor.GetParameters().Skip(1))
                    {
                        object? entity = Provider.GetService(parameter.ParameterType);
                        if (entity != null)
                        {
                            parameters.Add(entity);
                        }
                    }

                    if (parameters.Count == constructor.GetParameters().Length)
                    {
                        result = (Contracts.Validator.IValidator)constructor.Invoke(parameters.ToArray());
                        break;
                    }
                }
            }
        }
        
        return result;
    }
}