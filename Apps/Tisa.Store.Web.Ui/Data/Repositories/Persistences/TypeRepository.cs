using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Tisa.Store.Web.Ui.Data.Contexts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences;

public class TypeRepository : ITypeRepository
{
    protected ApplicationContext Context { get; }
    protected DbSet<Models.Entities.Type> Set { get; }
    protected ApiContext Api { get; }
    protected IStringLocalizer<TypeRepository> Localizer { get; }

    public TypeRepository(ApplicationContext context, ApiContext api, IStringLocalizer<TypeRepository> localizer)
    {
        Context = context;
        Api = api;
        Localizer = localizer;

        Set = context.Set<Models.Entities.Type>();
    }

    public async Task<IEnumerable<Models.DataTransfers.TypeDto>> Get(System.Func<Models.DataTransfers.TypeDto, bool>? predicate = null)
    {
        IEnumerable<Models.DataTransfers.Api.TypeDto> types = await Api.Types.Get();

        IEnumerable<Models.DataTransfers.TypeDto> result = await Set
            .Where(type => types.Select(api => api.Id).Contains(type.TypeId))
            .Select(type => new Models.DataTransfers.TypeDto()
            {
                Id = type.Id,
                Display = type.Name,
                TypeId = type.TypeId,
            })
        .ToListAsync();

        result = result.Select(type =>
            type with { Name = types.Where(dto => dto.Id == type.TypeId).Select(dto => dto.Name).First() });

        return predicate != null ? result.Where(predicate: predicate) : result;
    }

    /// <summary>
    /// Fetching only one Type from database
    /// </summary>
    /// <param name="primary"><see cref="int"/>Primary key in database</param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">Happened when send wrong Id (Primary key)</exception>
    public async Task<Models.DataTransfers.TypeDto> Get(int primary)
    {
        Models.DataTransfers.TypeDto? result = await Set
            .Where(type => type.Id == primary)
            .Select(type => new Models.DataTransfers.TypeDto()
            {
                Id = type.Id,
                Display = type.Name,
                TypeId = type.TypeId
            })
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        Models.DataTransfers.Api.TypeDto? dto = await Api.Types.Get(id: result.TypeId);

        if (dto == null)
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        return result with { Name = dto.Name };
    }

    /// <summary>
    /// Adding new Type to database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.TypeDto"/></param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">When throw the exception send invalid TypeId or exist TypeId</exception>
    public async Task<Models.DataTransfers.TypeDto> Add(Models.DataTransfers.TypeDto entry)
    {
        Models.DataTransfers.Api.TypeDto? dto = await Api.Types.Get(id: entry.TypeId);

        if (dto == null)
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        if (await Set.AnyAsync(type => type.TypeId == entry.TypeId))
        {
            throw new System.Exception(message: Localizer["ExistType"]);
        }

        Models.Entities.Type entity = new Models.Entities.Type()
        {
            Name = entry.Name,
            TypeId = entry.TypeId
        };

        await Set.AddAsync(entity);

        if (await Context.SaveChangesAsync() < 1)
        {
            throw new System.Exception(message: Localizer["SomethingWrongHappened"]);
        }

        return entry with { Id = entry.Id };
    }

    public Task<Models.DataTransfers.TypeDto> Update(Models.DataTransfers.TypeDto entity)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> Delete(Models.DataTransfers.TypeDto entity)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> Delete(int primary)
    {
        throw new System.NotImplementedException();
    }
}