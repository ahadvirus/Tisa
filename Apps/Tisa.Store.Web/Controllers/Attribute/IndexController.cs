using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.ViewModels.Attributes;

namespace Tisa.Store.Web.Controllers.Attribute;

[ApiController]
[Route(
    template: "[namespace]",
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
    public async Task<IEnumerable<IndexVM>> Invoke(CancellationToken cancellation)
    {
        return await Context.Attributes.Include(attribute => attribute.Type)
            .ProjectTo<IndexVM>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellation);
    }


    [HttpGet(template: "{" + nameof(id) + ":int}")]
    public async Task<ActionResult<IndexVM>> Invoke([FromRoute]int id, CancellationToken cancellation)
    {
        Expression<Func<Models.Entities.Attribute, bool>> predicate = attribute => attribute.Id == id;

        if (!await Context.Attributes
                .Where(predicate: predicate)
                .Select(selector: attribute => attribute.Id)
                .AnyAsync(cancellationToken: cancellation))
        {
            return BadRequest();
        }

        return Ok(await Context.Attributes
            .Where(predicate: predicate)
            .Include(attribute => attribute.Type)
            .ProjectTo<IndexVM>(Mapper.ConfigurationProvider)
            .FirstAsync(cancellation));
    }

}