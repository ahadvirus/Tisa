using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Tisa.Store.Web.Infrastructures.Routes;

public class ControllerNameDocumentationConvention : IControllerModelConvention
{
    public void Apply(ControllerModel? controller)
    {
        if (controller == null)
            return;

        foreach (SelectorModel selector in controller.Selectors.Where(selector => selector.AttributeRouteModel != null))
        {
            if (selector.AttributeRouteModel != null)
            {
                if (!string.IsNullOrWhiteSpace(selector.AttributeRouteModel.Name))
                    controller.ControllerName = selector.AttributeRouteModel.Name;
            }
        }
    }
}