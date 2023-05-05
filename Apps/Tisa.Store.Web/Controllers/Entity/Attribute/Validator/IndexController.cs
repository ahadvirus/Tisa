using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Entity.Attribute.Validator;

[ApiController]
[Route(
    template: nameof(Models.Entities.Entity) +
              "/{" +
              nameof(Models.ViewModels.Entities.Attributes.Validators.RequestVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}/" +
              nameof(Models.Entities.Attribute) +
              "/{" +
              nameof(Models.ViewModels.Entities.Attributes.Validators.RequestVM.Attribute) +
              ":" +
              nameof(Models.Entities.AttributeEntity) +
              "}/" +
              nameof(Models.Entities.Validator),
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
    public async Task<ActionResult> Index(
        [FromRoute] Models.ViewModels.Entities.Attributes.Validators.RequestVM entry,
        CancellationToken cancellationToken
    )
    {
        int attributeTypeId = await Context.Attributes
            .Where(attribute => attribute.Id == entry.AttributeId)
            .Select(attribute => attribute.TypeId)
            .FirstOrDefaultAsync(cancellationToken);

        if (attributeTypeId != 0)
        {
            return Ok(await Context.AttributeEntityValidators
                .Where(validator => validator.AttributeEntityId == entry.AttributeEntityId)
                .Where(attribute => attribute.Validator.Types.Any(type => type.TypeId == attributeTypeId))
                .ProjectTo<Models.ViewModels.Entities.Attributes.Validators.IndexVM>(
                    Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken));
        }

        return BadRequest();
    }
}