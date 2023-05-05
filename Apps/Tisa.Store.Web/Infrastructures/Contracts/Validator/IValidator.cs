using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Contracts.Validator;

public interface IValidator
{
    Task<bool> Validate(object? entry);

    Task<IDictionary<string, List<string>>> GetErrors();
}