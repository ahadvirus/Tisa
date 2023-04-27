using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Tisa.Store.Web.Infrastructures.Attributes;

namespace Tisa.Store.Web.Infrastructures.Routes;

public class NamespaceToken : IApplicationModelConvention
{
    private readonly string _tokenRegex;

    public NamespaceToken()
    {
        string tokenName = nameof(System.Type.Namespace).ToLower();
        _tokenRegex = $@"(\[{tokenName}])(?<!\[\1(?=]))";
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            string? @default = controller.Attributes
                .Where(attribute => attribute.GetType() == typeof(NamespaceAttribute))
                .Select(attribute => ((NamespaceAttribute)attribute).Default)
                .FirstOrDefault();
            
            string @namespace = !string.IsNullOrWhiteSpace(@default)
                ? (
                        !string.IsNullOrWhiteSpace(controller.ControllerType.Namespace)
                        ? controller.ControllerType.Namespace
                        : string.Empty
                )
                : string.Empty;
            
            if (!string.IsNullOrWhiteSpace(@namespace) && !string.IsNullOrWhiteSpace(@default))
            {
                @namespace = @namespace
                    .Replace(@default, string.Empty)
                    .Replace('.', '/');
                
                @namespace = @namespace[0] == '/' ? 
                    @namespace.Substring(1) : 
                    @namespace;

                @namespace = @namespace[(@namespace.Length - 1)] == '/'
                    ? @namespace.Substring(0, (@namespace.Length - 1))
                    : @namespace;
            }
            
            string tokenValue = @namespace;
            UpdateSelectors(controller.Selectors, tokenValue);
            UpdateSelectors(controller.Actions.SelectMany(a => a.Selectors), tokenValue);
        }
    }

    private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
    {
        foreach (var selector in selectors.Where(s => s.AttributeRouteModel != null))
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel.Template =
                    InsertTokenValue(selector.AttributeRouteModel.Template, tokenValue);
                
                selector.AttributeRouteModel.Name = 
                    InsertTokenValue(selector.AttributeRouteModel.Name, tokenValue);
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
            _tokenRegex, 
            string.IsNullOrWhiteSpace(tokenValue) ? string.Empty : tokenValue
            );
    }
}