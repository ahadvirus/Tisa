using System.Net.Http;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

public interface IApiRepository : IRepository
{
    /// <summary>
    /// Create connection to api
    /// </summary>
    /// <returns><see cref="HttpClient"/> The connection build to connect to api</returns>
    Task<HttpClient> ClientAsync();

    /// <summary>
    /// Save response as log to review older
    /// </summary>
    /// <param name="response"><see cref="string"/> The response from the api</param>
    /// <returns></returns>
    Task LogAsync(string response);
}