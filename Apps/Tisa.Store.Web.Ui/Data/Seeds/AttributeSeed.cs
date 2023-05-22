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

[Order(version: 20230520165010)]
public class AttributeSeed : ISeed<IAttributeRepository>
{
    private ITypeRepository TypeRepository { get; }

    private IApiAttributeRepository Api { get; }

    public AttributeSeed(ITypeRepository typeRepository, IApiAttributeRepository api)
    {
        TypeRepository = typeRepository;
        Api = api;
    }

    public async Task Invoke(IAttributeRepository repository)
    {
        IEnumerable<Models.DataTransfers.TypeDto> types = await TypeRepository.GetAsync(predicate: null);
        IEnumerable<Models.DataTransfers.AttributeDto> attributes = await repository.GetAsync(predicate: null);
        foreach (Models.DataTransfers.Api.AttributeDto item in await Api.GetAsync(attribute => !attributes.Select(selector: dto => dto.AttributeId).Contains(attribute.Id)))
        {
            try
            {
                await repository.SaveAsync(entry: new Models.DataTransfers.AttributeDto()
                {
                    Display = item.Title,
                    Description = item.Description,
                    AttributeId = item.Id,
                    TypeId = types
                        .Where(predicate: type => type.Name.Equals(item.Type))
                        .Select(selector: type => type.Id)
                        .FirstOrDefault()
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(message: string.Format(format: "\n{0}\n", args: new object?[] { e.Message }));
            }

        }
    }
}