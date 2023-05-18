using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences.Apis;

public class ApiRepository : IApiRepository
{
    protected ApiOption Option { get; }

    protected JsonSerializerOptions JsonOptions
    {
        get
        {
            return new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }

    protected string JsonMediaType
    {
        get
        {
            return "application/json";
        }
    }

    public ApiRepository(ApiOption option)
    {
        Option = option;
    }

    public Task<HttpClient> ClientAsync()
    {
        return Task.FromResult(new HttpClient() { BaseAddress = new Uri(Option.Address) });
    }

    public Task LogAsync(string response)
    {
        return Task.Run(() => Debug.Write(
            message: string.Format(
                format: "\n{0}\n",
                args: new object[] { response }
            )
        ));
    }
}