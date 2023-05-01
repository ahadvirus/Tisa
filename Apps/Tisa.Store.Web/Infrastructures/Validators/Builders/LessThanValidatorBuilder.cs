using System;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Tisa.Store.Web.Infrastructures.Validators.Builders;

public class LessThanValidatorBuilder : ValidatorBuilder
{
    public override Task<IPropertyValidator?> Build<T, TProperty>(object parameter)
    {
        throw new NotImplementedException();
    }
}