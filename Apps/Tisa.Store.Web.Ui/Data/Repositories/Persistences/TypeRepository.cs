using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Tisa.Store.Web.Ui.Data.Contexts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences;

public class TypeRepository : AbstractRepository<Models.Entities.Type, int>, ITypeRepository
{
    /// <summary>
    /// Access to Type in Api
    /// </summary>
    private IApiTypeRepository ApiSet { get; }

    /// <summary>
    /// Localization error to persian
    /// </summary>
    protected IStringLocalizer<TypeRepository> Localizer { get; }

    public TypeRepository(ApplicationContext context, ApiContext api, IStringLocalizer<TypeRepository> localizer) : base(context: context, apiContext: api)
    {
        Localizer = localizer;
        
        ApiSet = ApiContext.Types;
    }

    /// <summary>
    /// Get all types exist in api and local database
    /// </summary>
    /// <param name="predicate">For customize list before return</param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.DataTransfers.TypeDto>> GetAsync(System.Func<Models.DataTransfers.TypeDto, bool>? predicate = null)
    {
        IEnumerable<Models.DataTransfers.Api.TypeDto> types = await ApiSet.GetASync();

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
    public async Task<Models.DataTransfers.TypeDto> GetAsync(int primary)
    {

        if (!await ExistAsync(primary))
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        Models.DataTransfers.TypeDto result = await Set
            .Where(type => type.Id == primary)
            .Select(type => new Models.DataTransfers.TypeDto()
            {
                Id = type.Id,
                Display = type.Name,
                TypeId = type.TypeId
            })
            .FirstAsync();

        Models.DataTransfers.Api.TypeDto? dto = await ApiSet.GetAsync(id: result.TypeId);

        return result with { Name = dto?.Name ?? string.Empty };
    }

    /// <summary>
    /// Adding new Type to database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.TypeDto"/></param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">When throw the exception send invalid TypeId or exist TypeId</exception>
    public async Task<Models.DataTransfers.TypeDto> AddAsync(Models.DataTransfers.TypeDto entry)
    {
        if (!await ValidateAsync(type: entry.TypeId))
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        if (await Registered(type: entry.TypeId))
        {
            throw new System.Exception(message: Localizer["ExistType"]);
        }

        Models.DataTransfers.Api.TypeDto? dto = await ApiSet.GetAsync(id: entry.TypeId);

        Models.Entities.Type entity = new Models.Entities.Type()
        {
            Name = entry.Display,
            TypeId = entry.TypeId
        };

        await Set.AddAsync(entity);

        if (await Context.SaveChangesAsync() < 1)
        {
            throw new System.Exception(message: Localizer["SomethingWrongHappened"]);
        }

        return entry with { Id = entry.Id, Name = dto?.Name ?? string.Empty };
    }

    /// <summary>
    /// Update translation of type in local database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.TypeDto"/></param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">When throw the entry is invalid in both side</exception>
    public async Task<Models.DataTransfers.TypeDto> UpdateAsync(Models.DataTransfers.TypeDto entry)
    {
        if (!await ValidAsync(id: entry.Id, type: entry.TypeId))
        {
            throw new System.Exception(message: Localizer["InvalidType"]);
        }

        Models.Entities.Type entity = new Models.Entities.Type()
        {
            Id = entry.Id,
            Name = entry.Display,
            TypeId = entry.TypeId
        };

        Set.Update(entity);

        if (await Context.SaveChangesAsync() < 1)
        {
            throw new System.Exception(message: Localizer["SomethingWrongHappened"]);
        }

        return entry;
    }

    /// <summary>
    /// Checking type exist in both side
    /// </summary>
    /// <param name="id"><see cref="int"/>Primary key of type in local database</param>
    /// <returns><see cref="bool"/></returns>
    public async Task<bool> ExistAsync(int id)
    {
        int result = await Set.Where(type => type.Id == id).Select(type => type.TypeId).FirstOrDefaultAsync();
        return result != 0 && await ValidateAsync(result);
    }

    /// <summary>
    /// Checking type to point accurate type in local database
    /// </summary>
    /// <param name="id"><see cref="int"/>Primary key of type in local database</param>
    /// <param name="type"><see cref="int"/>Index key of type in local database</param>
    /// <returns><see cref="bool"/></returns>
    public async Task<bool> ValidAsync(int id, int type)
    {
        return await ExistAsync(id) && await Set
            .Where(model => model.Id == id && model.TypeId == type)
            .Select(model => model.Id)
            .AnyAsync();
    }

    /// <summary>
    /// Checking type validate in api side
    /// </summary>
    /// <param name="type"><see cref="int"/> The id (primary key) of type</param>
    /// <returns><see cref="bool"/></returns>
    protected async Task<bool> ValidateAsync(int type)
    {
        return await ApiSet.GetAsync(id: type) != null;
    }

    /// <summary>
    /// Checking type was add in local database
    /// </summary>
    /// <param name="type"><see cref="int"/> The id (primary key) of the type</param>
    /// <returns><see cref="bool"/></returns>
    protected async Task<bool> Registered(int type)
    {
        return await ValidateAsync(type: type) && await Set
            .Where(predicate: entity => entity.TypeId == type)
            .Select(selector: entity => entity.Id)
            .AnyAsync();
    }

    protected override void MapperConfiguration(IMapperConfigurationExpression configuration)
    {
        
    }
}