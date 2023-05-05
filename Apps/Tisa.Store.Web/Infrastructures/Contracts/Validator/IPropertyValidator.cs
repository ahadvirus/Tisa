using System.Reflection;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Infrastructures.Contracts.Validator;

public interface IPropertyValidator
{
    MethodInfo Method { get; }
    object?[]? Parameters { get; }

    Task Add(object rule);
}