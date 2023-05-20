using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Ui.Infrastructures.Extensions;

public static class RemoveViewComponentExtension
{
    public static string RemoveViewComponent(this string entry)
    {
        return entry.Replace(
            oldValue: nameof(ViewComponent),
            newValue: string.Empty
        );
    }
}