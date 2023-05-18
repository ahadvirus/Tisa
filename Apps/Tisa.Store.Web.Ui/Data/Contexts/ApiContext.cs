using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

namespace Tisa.Store.Web.Ui.Data.Contexts;

public class ApiContext
{

    public ApiContext(
        IApiTypeRepository types,
        IApiAttributeRepository attributes
        )
    {
        Types = types;
        Attributes = attributes;
    }

    public IApiTypeRepository Types { get; }
    public IApiAttributeRepository Attributes { get; }
}