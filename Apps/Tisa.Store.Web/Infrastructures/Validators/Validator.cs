using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Infrastructures.Validators;

public class Validator<T> : Contracts.Validator.IValidator
{
    private IEnumerable<IAttributeEntityDTO> AttributeValidator { get; }
    
    private InlineValidator<T> InlineValidator { get; }
    
    private ValidationResult? ValidationResult { get; set; }
    
    private bool Initialization { get; set; }

    public Validator(IEnumerable<IAttributeEntityDTO> attributeValidator)
    {
        AttributeValidator = attributeValidator;
        
        ValidationResult = null;

        InlineValidator = new InlineValidator<T>();

        Initialization = false;
    }

    private async Task Initial()
    {
        Type instanceType = typeof(T);
        
        ParameterExpression instance = Expression.Parameter(instanceType, nameof(Validate).ToLower());
        
        foreach (IAttributeEntityDTO attributeValidator in AttributeValidator)
        {
            if (attributeValidator.GetType == null)
            {
                continue;
            }

            // Create expression

            //Expression<Func<Models.ViewModels.Products.CreateVM, int>> expressionType = p => p.Name;

            //System.Type getExpressionType = expressionType.GetType();

            Type propertyType = attributeValidator.GetType;
            
            Type propertyFunc = typeof(Func<,>).MakeGenericType(instanceType, propertyType);

            MemberExpression property = Expression.Property(instance, attributeValidator.Name);
            
            LambdaExpression expression = Expression.Lambda(propertyFunc, property, instance);
            
            
            // Find RuleFor method

            MethodInfo? ruleFor = this.GetType()
                .GetMethod(nameof(RuleFor));

            if (ruleFor == null)
            {
                continue;
            }

            object? ruleBuilder = ruleFor.MakeGenericMethod(new Type[] { propertyType })
                .Invoke(this, new object?[] { expression });
            
            // Add property to InLineValidator

            //object? ruleBuilder = ruleFor.Invoke(InlineValidator, new object?[] { expression });

            if (ruleBuilder == null)
            {
                continue;
            }

            // Trying to create PropertyValidator for adding RuleBuilder
            
            foreach (IAttributeValidatorDTO validator in attributeValidator.Validators)
            {
                ValidatorRule<T> rule = new ValidatorRule<T>(attributeValidator, AttributeValidator, validator);

                IPropertyValidator? propertyValidator = await rule.GetRule();

                if (propertyValidator != null)
                {
                    
                    // Adding PropertyValidator to RuleBuilder
                    await propertyValidator.Add(ruleBuilder);
                    /*
                    MethodInfo? setValidator = typeof(IRuleBuilderInitial<,>).GetMethod(
                        nameof(IRuleBuilderInitial<dynamic, int>.SetValidator)
                    );
                    
                    if (setValidator == null)
                    {
                        continue;
                    }
                    
                    setValidator = setValidator.MakeGenericMethod(new System.Type[] { instanceType, propertyType });
                    setValidator.Invoke(ruleBuilder, new object?[] { propertyValidator });
                    */
                }
            }
        }
        Initialization = true;
    }

    public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(
        Expression<Func<T, TProperty>> expression)
    {
        return InlineValidator.RuleFor(expression);
    }

    public async Task<bool> Validate(dynamic entry)
    {
        if (Initialization == false)
        {
            await Initial();
        }
        
        ValidationResult = await InlineValidator.ValidateAsync(entry);
        return ValidationResult.IsValid;
    }

    public Task<IDictionary<string, List<string>>> GetErrors()
    {
        return Task.Run<IDictionary<string, List<string>>>(() =>
        {
            IDictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            if (ValidationResult != null && ValidationResult.IsValid == false)
            {
                foreach (ValidationFailure failure in ValidationResult.Errors)
                {
                    if (result.ContainsKey(failure.PropertyName) == false)
                    {
                        result.Add(failure.PropertyName, new List<string>());
                    }
                    
                    result[failure.PropertyName].Add(failure.ErrorMessage);
                }
            }
            return result;
        });
    }
}