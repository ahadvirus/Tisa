using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.Entities;
using Tisa.Store.Web.Models.ViewModels.Entities;

namespace Tisa.Store.Web.Controllers.Entity;

[ApiController]
[Route(
    template: "[namespace]",
    Name = "[namespace]"
)]
public class CreateController : ControllerBase
{
    
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public CreateController(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<IndexVM>> Invoke(CreateVM entry, CancellationToken cancellationToken)
    {
        IDictionary<string, Models.Entities.Attribute?> attributes = new Dictionary<string, Models.Entities.Attribute?>();

        foreach (string name in entry.Attributes)
        {
            attributes.Add(
                name,
                await Context.Attributes
                    .Where(attribute => attribute.Name == name)
                    .Include(attribute => attribute.Type)
                    .FirstOrDefaultAsync(cancellationToken)
            );
        }

        if (attributes.Any(pair => pair.Value == null))
        {
            foreach (string error in attributes.Where(pair => pair.Value == null).Select(pair => string.Format(
                             "Please send valid `Attribute`, we couldn't find `{0}` attribute", pair.Key
                         )
                     ))
            {
                ModelState.AddModelError(
                    key: nameof(CreateVM.Attributes),
                    errorMessage: error
                );
            }
            
            return BadRequest(ModelState);
        }

        if (await Context.Entities
                .Where(entity => entity.Name == entry.Name)
                .Select(entity => entity.Id)
                .AnyAsync(cancellationToken))
        {
            ModelState.AddModelError(
                key: nameof(CreateVM.Name),
                errorMessage: string.Format(
                    "The `{0}` entity already exist",
                    entry.Name
                    )
            );
            
            return BadRequest(ModelState);
        }

        Models.Entities.Entity entity = Mapper.Map<Models.Entities.Entity>(entry);

        entity.Attributes = new List<AttributeEntity>();

        foreach (Models.Entities.Attribute? attribute in attributes.Values)
        {
            if (attribute != null)
            {
                entity.Attributes.Add(new AttributeEntity()
                {
                    Attribute = attribute,
                    AttributeId = attribute.Id
                });
            }
        }

        await Context.Entities.AddAsync(entity, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return Ok(Mapper.Map<IndexVM>(entity));
    }
}