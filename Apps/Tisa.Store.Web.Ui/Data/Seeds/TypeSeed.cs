using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Infrastructures.Attributes;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Seeds;

[Order(version: 20230518102820)]
public class TypeSeed : ISeed<ITypeRepository>
{
    private IApiTypeRepository Api { get; }

    public TypeSeed(IApiTypeRepository api)
    {
        Api = api;
    }

    public async Task Invoke(ITypeRepository repository)
    {
        IEnumerable<Models.DataTransfers.TypeDto> types = await repository.GetAsync();

        foreach (Models.DataTransfers.Api.TypeDto type in (await Api.GetASync()).Where(type => !types.Select(dto => dto.TypeId).Contains(type.Id)))
        {
            try
            {
                await repository.AddAsync(entry: new Models.DataTransfers.TypeDto() { Name = type.Name, TypeId = type.Id });
            }
            catch (Exception e)
            {
                Debug.WriteLine(message: string.Format(format: "\n{0}\n", args: new object?[] { e.Message }), category: nameof(Exception));
            }

        }
    }
}