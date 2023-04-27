using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.ViewModels.Entities;

namespace Tisa.Store.Web.Controllers.Entity;

[ApiController]
[Route(template: "[namespace]", Name = "[namespace]")]
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
    public async Task<IEnumerable<IndexVM>> Invoke(CancellationToken cancellationToken)
    {
        return await Context.Entities
            .Include(product => product.Attributes)
            .ThenInclude(attribute => attribute.Attribute)
            .ProjectTo<IndexVM>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}