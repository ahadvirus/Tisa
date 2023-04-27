using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Attributes;
using Tisa.Store.Web.Models.Entities;
using Tisa.Store.Web.Models.ViewModels.Entities;

namespace Tisa.Store.Web.Controllers.Entity;

[ApiController]
[Namespace("Tisa.Store.Web.Controllers")]
[Route("[namespace]")]
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
        IDictionary<string, Attribute?> attributes = new Dictionary<string, Attribute?>();

        foreach (string name in entry.Attributes)
        {
            attributes.Add(
                name,
                await Context.Attributes
                    .Where(attribute => attribute.Name == name)
                    .FirstOrDefaultAsync(cancellationToken)
            );
        }

        if (attributes.Select(pair => pair.Value == null).Any())
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

        Models.Entities.Entity entity = Mapper.Map<Models.Entities.Entity>(entry);

        entity.Attributes = new List<AttributeEntity>();

        foreach (Attribute? attribute in attributes.Values)
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