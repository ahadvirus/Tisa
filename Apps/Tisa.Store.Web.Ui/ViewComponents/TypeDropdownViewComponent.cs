using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Models.ViewModels.ViewComponents;

namespace Tisa.Store.Web.Ui.ViewComponents;

public class TypeDropdownViewComponent : ViewComponent
{
    public TypeDropdownViewComponent(ITypeRepository repository)
    {
        Repository = repository;
    }

    protected ITypeRepository Repository { get; }

    public async Task<IViewComponentResult> InvokeAsync(int selected, string input)
    {
        return View(model: new TypeDropdownVm(
            options: (await Repository.GetAsync(predicate: null))
                .ToDictionary(
                    keySelector: entity => entity.Id,
                    elementSelector: entity => entity.Display
                )
            )
        {
            Selected = selected == 0 ? 1 : selected,
            Input = input

        });
    }
}