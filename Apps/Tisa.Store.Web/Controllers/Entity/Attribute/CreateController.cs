using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Entity.Attribute;

[ApiController]
[Route(
    template: (nameof(Models.Entities.Entity) + "/{" + 
               nameof(Models.ViewModels.Entities.Attributes.IndexVM.Name) + ":" + 
               nameof(Models.Entities.Entity) + "}/" + 
               nameof(Models.Entities.Attribute)),
    Name = "[namespace].[controller]"
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
    public async Task<ActionResult<Models.ViewModels.Attributes.IndexVM>> Invoke(
        [FromRoute] Models.ViewModels.Entities.Attributes.IndexVM entity,
        [FromBody] Models.ViewModels.Entities.Attributes.CreateVM entry,
        CancellationToken cancellationToken
    )
    {
        Models.Entities.Attribute? attribute = await Context.Attributes
            .Where(attribute => attribute.Name == entry.Title)
            .Include(attribute => attribute.Entites.Where(attributeEntity => attributeEntity.EntityId == entity.Id))
            .Include(attribute => attribute.Type)
            .FirstOrDefaultAsync(cancellationToken);

        if (attribute == null)
        {
            ModelState.AddModelError(
                nameof(Models.Entities.Attribute),
                string.Format(
                    "Please send valid `Attribute`, we couldn't find `{0}` attribute",
                    entry.Title
                )
            );

            return BadRequest(ModelState);
        }

        if (attribute.Entites.Any())
        {
            ModelState.AddModelError(
                nameof(Models.Entities.Attribute),
                string.Format(
                    "The `{0}` attribute already bounded to `{1}`",
                    entry.Title,
                    entity.Name
                )
            );

            return BadRequest(ModelState);
        }

        Models.Entities.AttributeEntity attributeEntity = new Models.Entities.AttributeEntity()
        {
            Attribute = attribute,
            AttributeId = attribute.Id,
            EntityId = entity.Id
        };

        await Context.AttributeEntities.AddAsync(attributeEntity, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return Ok(Mapper.Map<Models.ViewModels.Attributes.IndexVM>(attributeEntity));
    }
}