using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Entity.Attribute;

[ApiController]
[Route(
    template: nameof(Models.Entities.Entity) + "/{name:entity}/" + nameof(Models.Entities.Attribute),
    Name = "[namespace]"
)]
public class IndexController : ControllerBase
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public IndexController(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<Models.ViewModels.Attributes.IndexVM>> Invoke(
        [FromRoute]Models.ViewModels.Entities.Attributes.IndexVM entry, 
        CancellationToken cancellationToken
        )
    {
        IQueryable<Models.ViewModels.Attributes.IndexVM> query = Context.Entities
            .Where(entity => entity.Name == entry.Name)
            .SelectMany(entity => entity.Attributes)
            .Include(attribute => attribute.Attribute)
            .ThenInclude(attribute => attribute.Type)
            .ProjectTo<Models.ViewModels.Attributes.IndexVM>(Mapper.ConfigurationProvider);
        
        Debug.WriteLine(query.ToString());
        
        return await query
            .ToListAsync(cancellationToken);
    }
}