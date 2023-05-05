using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;

namespace Tisa.Store.Web.Infrastructures.Validators;

public abstract class ValidatorBuilder : ValidatorAttribute, IValidatorBuilder
{
    public abstract Task<IPropertyValidator?> Build<T, TProperty>(object parameter);

    public Expression<Func<T, TProperty>> BuildExpression<T, TProperty>(string attribute)
    {
        Type instanceType = typeof(T);
        
        ParameterExpression instance = Expression.Parameter(instanceType, nameof(Models.Entities.Product).ToLower());

        Type propertyType = typeof(TProperty);
            
        Type propertyFunc = typeof(Func<,>).MakeGenericType(instanceType, propertyType);
        
        MemberExpression property = Expression.Property(instance, attribute);
            
        return (Expression<Func<T, TProperty>>)Expression.Lambda(propertyFunc, property, instance);
    }
}