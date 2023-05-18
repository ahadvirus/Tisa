using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;
using Tisa.Store.Web.Ui.Models.DataTransfers.Api;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences.Apis;

public class ApiAttributeRepository : ApiRepository, IApiAttributeRepository
{
    protected string Address
    {
        get
        {
            return "Attribute";
        }
    }

    protected IStringLocalizer<ApiAttributeRepository> Localizer { get; }

    public ApiAttributeRepository(ApiOption option, IStringLocalizer<ApiAttributeRepository> localizer) : base(option)
    {
        Localizer = localizer;
    }

    public async Task<IEnumerable<AttributeDto>> GetAsync(Func<AttributeDto, bool>? predicate = null)
    {
        IEnumerable<AttributeDto> result = new List<AttributeDto>();

        using (HttpClient client = await ClientAsync())
        {
            using (HttpResponseMessage message = await client.GetAsync(Address))
            {
                await LogAsync(await message.Content.ReadAsStringAsync());

                IEnumerable<AttributeDto>? content = await JsonSerializer.DeserializeAsync<IEnumerable<AttributeDto>>(
                    utf8Json: await message.Content.ReadAsStreamAsync(),
                    options: JsonOptions
                );

                if (content != null)
                {
                    result = predicate != null ? content.Where(predicate: predicate) : content;
                }
            }
        }

        return result;
    }

    public async Task<AttributeDto?> GetAsync(int id)
    {
        AttributeDto? result;

        using (HttpClient client = await ClientAsync())
        {
            using (HttpResponseMessage message = await client.GetAsync(requestUri:
                       string.Format(
                           format: "{0}/{1}",
                           args: new object?[] { Address }
                           )
                       )
                   )
            {
                await LogAsync(await message.Content.ReadAsStringAsync());

                result = await JsonSerializer.DeserializeAsync<AttributeDto>(
                    utf8Json: await message.Content.ReadAsStreamAsync(),
                    options: JsonOptions
                );
            }
        }

        return result;
    }

    public async Task<AttributeDto> AddAsync(AttributeDto entry)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(utf8Json: stream, value: entry, options: JsonOptions);

            using (StreamReader reader = new StreamReader(stream))
            {
                using (HttpClient client = await ClientAsync())
                {
                    using (HttpResponseMessage message = await client.PostAsync(
                               requestUri: Address,
                               content: new StringContent(
                                   content: await reader.ReadToEndAsync(),
                                   encoding: Encoding.UTF8,
                                   mediaType: JsonMediaType
                               )
                           )
                          )
                    {
                        if (message.IsSuccessStatusCode)
                        {
                            await LogAsync(await message.Content.ReadAsStringAsync());

                            AttributeDto? content = await JsonSerializer.DeserializeAsync<AttributeDto>(
                                utf8Json: await message.Content.ReadAsStreamAsync(),
                                options: JsonOptions
                            );

                            if (content != null)
                            {
                                entry = content;
                            }
                            else
                            {
                                throw new Exception(Localizer["SomethingWrongHappened"]);
                            }
                        }
                        else
                        {
                            throw new Exception(await message.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
        }

        return entry;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result;

        using (HttpClient client = await ClientAsync())
        {
            using (HttpResponseMessage message = await client.DeleteAsync(
                       requestUri: string.Format(
                           format: "{0}/{1}",
                           args: new object?[] { Address, id }
                           )
                       )
                   )
            {
                result = message.IsSuccessStatusCode;
            }
        }

        return result;
    }
}