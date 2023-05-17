using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

namespace Tisa.Store.Web.Ui.Data.Contexts;

public class ApiContext
{

    public ApiContext(IApiTypeRepository types)
    {
        Types = types;
    }

    public IApiTypeRepository Types { get; }
}