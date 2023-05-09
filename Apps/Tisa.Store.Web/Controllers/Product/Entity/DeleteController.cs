using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.ViewModels.Products;

namespace Tisa.Store.Web.Controllers.Product.Entity;

[ApiController]
[Route(
    template: nameof(Models.Entities.Product) +
              "/{" +
              nameof(DeleteVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}" +
              "/{" +
              nameof(DeleteVM.Id) +
              ":" +
              "int" +
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

    public async Task<IActionResult> Invoke(
        [FromRoute] DeleteVM entry,
        CancellationToken cancellationToken
        )
    {
        IQueryable<Models.Entities.Product> products = Context.Products
            .Where(product => product.EntityId == entry.EntityId)
            .Where(product => product.Group == entry.Id);

        if (await products.AnyAsync(cancellationToken))
        {
            Context.Products.RemoveRange(await products.ToListAsync(cancellationToken));
            await Context.SaveChangesAsync(cancellationToken);
        }
        
        return Ok();
    }
}