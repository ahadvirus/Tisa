using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;

namespace Tisa.Store.Web.Ui.Controllers;

public class TypesController : Controller
{
    public ITypeRepository Repository { get; }

    public TypesController(ITypeRepository repository)
    {
        Repository = repository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await Repository.Get());
    }
}