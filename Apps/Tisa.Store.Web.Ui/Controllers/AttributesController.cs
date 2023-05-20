using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Models.DataTransfers;
using Tisa.Store.Web.Ui.Models.ViewModels.Attributes;

namespace Tisa.Store.Web.Ui.Controllers;

public class AttributesController : Controller
{
    private IAttributeRepository Repository { get; }

    public AttributesController(IAttributeRepository repository)
    {
        Repository = repository;
    }


    // GET
    public async Task<IActionResult> Index([FromQuery] Models.ViewModels.Search.RequestVm request)
    {
        Func<Models.DataTransfers.AttributeDto, bool>? predicate = null;

        if (!string.IsNullOrEmpty(request.Query))
        {
            predicate = dto => dto.Display.Contains(request.Query);
        }

        return View(model: new Models.ViewModels.Search.ResponseVm<Models.DataTransfers.AttributeDto>(
            results: await Repository.GetAsync(predicate: predicate)
            )
        {
            Query = request.Query
        });
    }

    public IActionResult Edit([FromRoute] int? id)
    {
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [ActionName(name:nameof(Create))]
    [HttpPost]
    public async Task<IActionResult> CreateConfirmed(
        [Bind(include: new string[] { nameof(CreateVm.Display), nameof(CreateVm.Description), nameof(CreateVm.Type) })]
        CreateVm entry
    )
    {
        IActionResult result = View(model: entry);

        if (ModelState.IsValid)
        {
            try
            {
                await Repository.AddAsync(entry: new AttributeDto()
                {
                    Display = entry.Display,
                    Description = entry.Description,
                    TypeId = entry.Type
                });

                result = RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(key: "SomethingHappened", errorMessage: e.Message);
            }
        }

        return result;
    }
}