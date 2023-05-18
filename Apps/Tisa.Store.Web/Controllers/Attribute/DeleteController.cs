using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Attribute;

[ApiController]
[Route(template: "[namespace]", Name = "[namespace]")]
public class DeleteController : ControllerBase
{
    protected ApplicationContext Context { get; }

    public DeleteController(ApplicationContext context)
    {
        Context = context;
    }

    [HttpDelete]
    public async Task<ActionResult> Index([FromRoute]int id, CancellationToken cancellation)
    {
        Expression<Func<Models.Entities.Attribute, bool>> predicate = attribute => attribute.Id == id;
        if (!await Context.Attributes
                .Where(predicate: predicate)
                .Select(selector: attribute => attribute.Id)
                .AnyAsync(cancellationToken: cancellation))
        {
            return BadRequest();
        }

        Models.Entities.Attribute attribute = await Context.Attributes.FirstAsync(predicate: predicate, cancellationToken: cancellation);

        Context.Attributes.Remove(attribute);

        await Context.SaveChangesAsync(cancellationToken: cancellation);

        return Ok();
    }
}