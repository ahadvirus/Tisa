using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tisa.Store.Web.Ui.Models.ViewModels.Search;

namespace Tisa.Store.Web.Ui.ViewComponents;

public class SearchFormViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(BaseVm entry)
    {
        return Task.FromResult<IViewComponentResult>(View(entry));
    }
}