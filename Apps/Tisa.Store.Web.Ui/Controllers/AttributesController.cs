using System;
using System.Linq.Expressions;
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
        Expression<Func<AttributeDto, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(request.Query))
        {
            predicate = dto => dto.Display.Contains(request.Query);
        }

        return View(model: new Models.ViewModels.Search.ResponseVm<AttributeDto>(
            results: await Repository.GetAsync(predicate: predicate)
            )
        {
            Query = request.Query
        });
    }

    public async Task<IActionResult> Edit([FromRoute] int? id)
    {
        IActionResult result = RedirectToAction(nameof(Index));

        if (id != null && await Repository.ExistAsync(id: id.Value))
        {
            AttributeDto? entity = await Repository.GetAsync(id: id.Value);

            if (entity != null)
            {
                EditVm model = new EditVm()
                {
                    Id = entity.Id,
                    Display = entity.Display,
                    Description = entity.Description
                };
                result = View(model: model);
            }

            
        }

        return result;
    }

    [ValidateAntiForgeryToken]
    [ActionName(name: nameof(Edit))]
    [HttpPost]
    public async Task<IActionResult> EditConfirmed(
        [Bind(include: new string[]
            { nameof(EditVm.Id), nameof(EditVm.Display), nameof(EditVm.Description) })
        ]
        EditVm entry
    )
    {
        IActionResult result = View(model: entry);

        if (ModelState.IsValid)
        {
            AttributeDto entity = new AttributeDto()
            {
                Id = entry.Id,
                Display = entry.Display,
                Description = entry.Description,
            };

            try
            {
                await Repository.UpdateAsync(entity);
                result = RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        return result;
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