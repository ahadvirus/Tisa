using System;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Contracts.Validator;

public interface IValidatorParameter
{
    Task<bool> Validate(object? parameter, Type attribute, Func<string, Type> attributeFinder);
}