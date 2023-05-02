using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
              nameof(Models.Entities.Validator) +
              "/{" +
              nameof(Models.ViewModels.Entities.Attributes.Validators.RequestVM.Validator) +
              ":" +
              nameof(Models.Entities.AttributeEntityValidator) +
              "}",
    Name = "[namespace].[controller]"
)]
public class DeleteController : ControllerBase
{
    private ApplicationContext Context { get; }

    public DeleteController(ApplicationContext context)
    {
        Context = context;
    }
    
    [HttpDelete]
    public async Task<ActionResult> Invoke(
        [FromRoute] Models.ViewModels.Entities.Attributes.Validators.RequestVM request,
        CancellationToken cancellationToken
    )
    {
        Models.Entities.AttributeEntityValidator? attributeEntityValidator =
            await Context.AttributeEntityValidators.FindAsync(request.AttributeEntityValidatorId, cancellationToken);
        if (attributeEntityValidator != null)
        {
            Context.AttributeEntityValidators.Remove(attributeEntityValidator);
            await Context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }

        return BadRequest();
    }
}