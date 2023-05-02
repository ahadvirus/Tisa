using System.Collections.Generic;
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
    template: (nameof(Models.Entities.Entity) + "/{" +
               nameof(Models.ViewModels.Entities.Attributes.RequestVM.Entity) + ":" +
               nameof(Models.Entities.Entity) + "}/" +
               nameof(Models.Entities.Attribute)),
    Name = "[namespace].[controller]"
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
        [FromRoute] Models.ViewModels.Entities.Attributes.RequestVM request,
        CancellationToken cancellationToken
    )
    {
        return await Context.Entities
            .Where(entity => entity.Name == request.Entity)
            .SelectMany(entity => entity.Attributes)
            .Include(attribute => attribute.Attribute)
            .ThenInclude(attribute => attribute.Type)
            .ProjectTo<Models.ViewModels.Attributes.IndexVM>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}