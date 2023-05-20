using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Ui.Data.Contexts;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Persistences;

public abstract class AbstractRepository<TEntity, TPrimary> where TEntity : class, IEntity<TPrimary> where TPrimary : struct
{
    /// <summary>
    /// Local Database
    /// </summary>
    protected ApplicationContext Context { get; }

    /// <summary>
    /// Local Table
    /// </summary>
    protected DbSet<TEntity> Set { get; }

    /// <summary>
    /// Api Database
    /// </summary>
    protected ApiContext ApiContext { get; }

    /// <summary>
    /// For converting data
    /// </summary>
    protected IMapper Mapper { get; }


    protected AbstractRepository(ApplicationContext context, ApiContext apiContext)
    {
        Context = context;
        
        ApiContext = apiContext;

        Set = Context.Set<TEntity>();

        Mapper = new Mapper(configuration: new MapperConfiguration(configure: MapperConfiguration));
    }

    /// <summary>
    /// Configuration mapping
    /// </summary>
    /// <param name="configuration"><see cref="IMapperConfigurationExpression"/> To create all configuration you need in the class</param>
    protected abstract void MapperConfiguration(IMapperConfigurationExpression configuration);
}