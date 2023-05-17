﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;
using Tisa.Store.Web.Ui.Models.DataTransfers.Api;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences.Apis;

public class ApiTypeRepository : IApiTypeRepository
{
    protected ApiOption Option { get; }

    protected string Address
    {
        get
        {
            return "Type";
        }
    }

    public ApiTypeRepository(ApiOption option)
    {
        Option = option;
    }

    public async Task<IEnumerable<TypeDto>> Get(Func<TypeDto, bool>? predicate = null)
    {
        IEnumerable<TypeDto> result = new TypeDto[] { };

        using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Option.Address) })
        {
            using (HttpResponseMessage message = await client.GetAsync(requestUri: Address))
            {
                if (message.IsSuccessStatusCode)
                {
                    IEnumerable<TypeDto>?  content = JsonSerializer.Deserialize<IEnumerable<TypeDto>>(
                        await message.Content.ReadAsStringAsync());

                    if (content != null)
                    {
                        result = predicate != null ? content.Where(predicate: predicate) : content;
                    }
                }
            }
        }

        return result;
    }

    public async Task<TypeDto?> Get(int id)
    {
        TypeDto? result = null;

        using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Option.Address) })
        {
            using (HttpResponseMessage message = await client.GetAsync(requestUri: string.Format(format: "{0}/{1}", args: new object?[] { Address, id })))
            {
                if (message.IsSuccessStatusCode)
                {
                    result = JsonSerializer.Deserialize<TypeDto>(
                        await message.Content.ReadAsStringAsync());
                }
            }
        }

        return result;
    }
}