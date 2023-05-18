using System.Net.Http;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

public interface IApiRepository : IRepository
{
    Task<HttpClient> ClientAsync();
    Task LogAsync(string response);
}