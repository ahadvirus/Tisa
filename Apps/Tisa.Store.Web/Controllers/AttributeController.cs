using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.ViewModels.Attributes;

namespace Tisa.Store.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AttributeController : ControllerBase
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public AttributeController(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<IndexVM>> Index(CancellationToken cancellation)
    {
        return await Context.Attributes.Include(attribute => attribute.Type)
            .ProjectTo<IndexVM>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellation);
    }

    [HttpPost]
    public async Task<ActionResult<IndexVM>> Create(CreateVM entry, CancellationToken cancellation)
    {
        int typeId = await Context.Types.Where(type => type.Kind == entry.Type)
            .Select(type => type.Id)
            .FirstOrDefaultAsync(cancellation);

        if (typeId == 0)
        {
            ModelState.AddModelError(
                nameof(CreateVM.Type),  
                string.Format("Please send valid `{0}`", nameof(CreateVM.Type))
                );

            return BadRequest(ModelState);
        }

        Models.Entities.Attribute attribute = Mapper.Map<Models.Entities.Attribute>(entry);

        attribute.TypeId = typeId;

        await Context.Attributes.AddAsync(attribute, cancellation);

        await Context.SaveChangesAsync(cancellation);

        return Ok(Mapper.Map<IndexVM>(attribute));

    }
}