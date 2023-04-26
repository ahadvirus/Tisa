using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TypeController : ControllerBase
{
    private ApplicationContext Context { get; }

    public TypeController(ApplicationContext context)
    {
        Context = context;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok(await Context.Types.ToListAsync());
    }
}