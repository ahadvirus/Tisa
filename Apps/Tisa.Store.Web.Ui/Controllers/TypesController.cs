using System;
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
        return View(await Repository.GetAsync());
    }

    // GET
    public async Task<IActionResult> Edit([FromRoute] int? id)
    {
        IActionResult result;

        if (id == null)
        {
            result = RedirectToAction(actionName: nameof(Index));
        }
        else
        {
            try
            {

                Models.DataTransfers.TypeDto dto = await Repository.GetAsync((int)id);
                result = View(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = RedirectToAction(actionName: nameof(Index));
            }
        }

        
        return result;
    }

    [HttpPost]
    [ActionName(name: nameof(Edit))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditConfirmed([FromRoute]int? id, [FromForm] Models.DataTransfers.TypeDto entry)
    {
        IActionResult result;

        if (id == null)
        {
            result = RedirectToAction(actionName: nameof(Index));
        }
        else
        {
            if (!ModelState.IsValid)
            {
                result = View(entry);
            }
            else
            {
                try
                {
                    await Repository.UpdateAsync(entry);
                    result = RedirectToAction(actionName: nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = View(entry);
                }
            }
        }

        return result;
    }
}