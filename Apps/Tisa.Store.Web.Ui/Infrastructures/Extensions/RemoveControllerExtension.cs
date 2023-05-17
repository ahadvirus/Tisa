using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Ui.Infrastructures.Extensions;

public static class RemoveControllerExtension
{
    public static string RemoveController(this string controllerName)
    {
        return controllerName.Replace(oldValue: nameof(Controller), newValue: string.Empty);
    }
}