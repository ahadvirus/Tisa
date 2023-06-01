using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Ui.Data.Contexts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Models.DataTransfers;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences;

public class AttributeRepository : AbstractRepository<Models.Entities.Attribute, int>, IAttributeRepository
{
    protected IApiAttributeRepository ApiSet { get; }

    protected ITypeRepository TypeRepository { get; }


    public AttributeRepository(
        ApplicationContext context,
        ApiContext apiContext,
        ITypeRepository typeRepository
        ) : base(context, apiContext)
    {
        TypeRepository = typeRepository;
        ApiSet = ApiContext.Attributes;
    }


    public async Task<IEnumerable<AttributeDto>> GetAsync(Expression<Func<AttributeDto, bool>>? predicate = null)
    {
        IEnumerable<Models.DataTransfers.Api.AttributeDto> attributes = await ApiSet.GetAsync(predicate: null);

        IQueryable<AttributeDto> query = Set
            .Where(predicate: attribute => attributes.Select(dto => dto.Id).Contains(attribute.AttributeId))
            .ProjectTo<AttributeDto>(Mapper.ConfigurationProvider);


        if (predicate != null)
        {
            query = query.Where(predicate: predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<AttributeDto?> GetAsync(int id)
    {
        AttributeDto? result = null;

        if (await ExistAsync(id))
        {
            result = await Set.Where(predicate: attribute => attribute.Id == id)
                .ProjectTo<AttributeDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        return result;
    }

    public async Task<AttributeDto> AddAsync(AttributeDto entry)
    {
        if (!await TypeRepository.ExistAsync(id: entry.TypeId))
        {
            throw new Exception(message: "InvalidType");
        }

        TypeDto type = await TypeRepository.GetAsync(primary: entry.TypeId);
        
        Models.DataTransfers.Api.AttributeDto dto = await ApiSet.AddAsync(entry: new Models.DataTransfers.Api.AttributeDto(
                id: 0,
                title: entry.Display,
                description: entry.Description,
                type: type.TypeId.ToString()
            )
        );

        return await SaveAsync(entry: entry with{AttributeId = dto.Id});

    }

    public async Task<AttributeDto> SaveAsync(AttributeDto entry)
    {
        if (!await ValidateAsync(attribute: entry.AttributeId))
        {
            throw new Exception(message: "InvalidAttribute");
        }

        if (await Registered(attribute: entry.AttributeId))
        {
            throw new Exception(message: "ExistAttribute");
        }

        Models.Entities.Attribute entity = new Models.Entities.Attribute()
        {
            Name = entry.Display,
            Description = entry.Description,
            TypeId = entry.TypeId,
            AttributeId = entry.AttributeId
        };

        await Set.AddAsync(entity: entity);

        if (await Context.SaveChangesAsync() < 1)
        {
            throw new Exception(message: "SomethingWrongHappened");
        }

        return entry with { Id = entity.Id };
    }

    public async Task<AttributeDto> UpdateAsync(AttributeDto entry)
    {
        if (!await ExistAsync(id: entry.Id))
        {
            throw new Exception(message: "ValidAttribute");
        }

        Models.Entities.Attribute entity = await Set.FirstAsync(predicate: model => model.Id == entry.Id);

        entity.Name = entry.Display;
        entity.Description = entry.Description;
        
        Set.Update(entity: entity);

        if (await Context.SaveChangesAsync() < 1)
        {
            throw new Exception(message: "SomethingWrongHappened");
        }

        return entry;
    }

    public async Task<bool> DeleteAsync(AttributeDto entry)
    {
        bool result = await ExistAsync(id: entry.Id);

        if (result)
        {
            result = await ApiSet.DeleteAsync(id: entry.AttributeId);

            if (result)
            {
                Models.Entities.Attribute attribute =
                    await Set.FirstAsync(predicate: attribute => attribute.Id == entry.Id);

                Set.Remove(attribute);

                result = await Context.SaveChangesAsync() < 1;
            }
        }

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool result = await ExistAsync(id: id);

        if (result)
        {
            result = await DeleteAsync(entry: await GetAsync(id: id) ?? new AttributeDto());
        }

        return result;
    }

    public async Task<bool> ExistAsync(int id)
    {
        int attribute = await Set.Where(predicate: attribute => attribute.Id == id)
            .Select(selector: attribute => attribute.AttributeId)
            .FirstOrDefaultAsync();

        return attribute != 0 && (await ApiSet.GetAsync(attribute)) != null;
    }

    public async Task<bool> ValidAsync(int id, int attribute)
    {
        return await ExistAsync(id: id) && await Set
            .Where(predicate: entity => entity.Id == id && entity.AttributeId == attribute)
            .Select(selector: entity => entity.Id)
            .AnyAsync();
    }

    /// <summary>
    /// Checking attribute validate in api side
    /// </summary>
    /// <param name="attribute"><see cref="int"/> The id (primary key) of attribute</param>
    /// <returns><see cref="bool"/></returns>
    protected async Task<bool> ValidateAsync(int attribute)
    {
        return await ApiSet.GetAsync(id: attribute) != null;
    }

    /// <summary>
    /// Checking attribute was add in local database
    /// </summary>
    /// <param name="attribute"><see cref="int"/> The id (primary key) of the attribute</param>
    /// <returns><see cref="bool"/></returns>
    protected async Task<bool> Registered(int attribute)
    {
        return await ValidateAsync(attribute: attribute) && await Set
            .Where(predicate: entity => entity.AttributeId == attribute)
            .Select(selector: entity => entity.Id)
            .AnyAsync();
    }

    protected override void MapperConfiguration(IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<Models.Entities.Attribute, AttributeDto>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                )
            )
            .ForMember(des => des.AttributeId,
                opt => opt.MapFrom(
                    src => src.AttributeId
                )
            )
            .ForMember(des => des.Display,
                opt => opt.MapFrom(
                    src => src.Name
                )
            )
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Description
                )
            )
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Type.Name
                )
            )
            .ForMember(des => des.TypeId,
                opt => opt.MapFrom(
                    src => src.Type.Id
                )
            );
    }
}