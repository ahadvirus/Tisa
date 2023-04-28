﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Entity.Attribute;

[ApiController]
[Route(
    template: nameof(Models.Entities.Entity) + 
              "/{" + 
              nameof(Models.ViewModels.Entities.Attributes.DeleteVM.Entity) + 
              ":" + 
              nameof(Models.Entities.Entity) + 
              "}/" + 
              nameof(Models.Entities.Attribute) + 
              "/{" + 
              nameof(Models.ViewModels.Entities.Attributes.DeleteVM.Attribute) + 
              ":" + 
              nameof(Models.Entities.AttributeEntity) + 
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
    public async Task<IActionResult> Invoke(
        [FromRoute] Models.ViewModels.Entities.Attributes.DeleteVM entry,
        CancellationToken cancellationToken
    )
    {
        Models.Entities.AttributeEntity? attributeEntity =
            await Context.AttributeEntities.FindAsync(entry.AttributeEntityId, cancellationToken);
        if (attributeEntity != null)
        {
            Context.AttributeEntities.Remove(attributeEntity);
            await Context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }

        return BadRequest();
    }
}