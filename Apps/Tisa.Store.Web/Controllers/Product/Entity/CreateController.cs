using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Controllers.Product.Entity;

[ApiController]
[Route(
    template: nameof(Models.Entities.Product) +
              "/{" +
              nameof(Models.ViewModels.Products.RequestVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}",
    Name = "[namespace].[controller]"
)]
public class CreateController : ControllerBase
{
    [HttpPost]
    public ActionResult Invoke(
        [FromRoute] Models.ViewModels.Products.RequestVM request,
        [FromBody] dynamic entry,
        CancellationToken cancellationToken
    )
    {
        return Ok(entry);
    }
}