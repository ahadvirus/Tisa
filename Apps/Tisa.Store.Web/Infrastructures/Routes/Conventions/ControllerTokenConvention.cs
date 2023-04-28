using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Tisa.Store.Web.Infrastructures.Routes.Conventions;

public class ControllerTokenConvention : IApplicationModelConvention
{
    private string TokenRegex { get; }
    private string TokenName { get; }

    public ControllerTokenConvention()
    {
        TokenName = nameof(Microsoft.AspNetCore.Mvc.Controller);
        TokenRegex = $@"(\[{TokenName.ToLower()}])(?<!\[\1(?=]))";
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            string tokenValue = controller.ControllerType.Name.Replace(TokenName, string.Empty);

            UpdateSelectors(controller.Selectors, tokenValue);
            UpdateSelectors(controller.Actions.SelectMany(a => a.Selectors), tokenValue);
        }
    }

    private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
    {
        foreach (SelectorModel selector in selectors.Where(selector => selector.AttributeRouteModel != null))
        {
            if (selector.AttributeRouteModel != null)
            {
                AttributeRouteModel? @new = AttributeRouteModel.CombineAttributeRouteModel(
                    selector.AttributeRouteModel,
                    new AttributeRouteModel()
                );
                
                if (@new != null)
                {
                    @new.Name =
                        InsertTokenValue(@new.Name, tokenValue);
                }

                selector.AttributeRouteModel = @new;
            }
        }
    }

    private string? InsertTokenValue(string? template, string? tokenValue)
    {
        if (template is null)
        {
            return template;
        }

        return Regex.Replace(
            template,
            TokenRegex,
            string.IsNullOrWhiteSpace(tokenValue) ? string.Empty : tokenValue
        );
    }
}