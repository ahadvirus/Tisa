using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Tisa.Store.Web.Infrastructures.Attributes;

namespace Tisa.Store.Web.Infrastructures.Routes;

public class NamespaceToken : IApplicationModelConvention
{
    private string TokenRegex { get; }
    private string TokenName { get; }
    private string TokenValue { get; }

    public NamespaceToken(string tokenValue)
    {
        TokenValue = tokenValue;
        TokenName = nameof(System.Type.Namespace).ToLower();
        TokenRegex = $@"(\[{TokenName}])(?<!\[\1(?=]))";
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            string? @default = controller.Attributes
                .Where(attribute => attribute.GetType() == typeof(NamespaceAttribute))
                .Select(attribute => ((NamespaceAttribute)attribute).Default)
                .FirstOrDefault();

            @default = string.IsNullOrWhiteSpace(@default)
                ? (
                    string.IsNullOrWhiteSpace(TokenValue)
                        ? string.Empty
                        : TokenValue
                )
                : @default;
            
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
                        .Replace(@default, string.Empty);
                
                if (!string.IsNullOrWhiteSpace(@namespace))
                {
                    @namespace = @namespace[0] == '.' ? @namespace.Substring(1) : @namespace;

                    @namespace = @namespace[(@namespace.Length - 1)] == '.'
                        ? @namespace.Substring(0, (@namespace.Length - 1))
                        : @namespace;
                }
            }

            UpdateSelectors(controller.Selectors, @namespace);
            UpdateSelectors(controller.Actions.SelectMany(a => a.Selectors), @namespace);
        }
    }

    private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
    {
        foreach (SelectorModel selector in selectors.Where(selector => selector.AttributeRouteModel != null))
        {
            if (selector.AttributeRouteModel != null)
            {
                AttributeRouteModel? @new = AttributeRouteModel.CombineAttributeRouteModel(selector.AttributeRouteModel, new AttributeRouteModel());
                if (@new != null)
                {
                    @new.Template =
                        InsertTokenValue(@new.Template, ParseToRouteUrl(tokenValue));
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

    private string? ParseToRouteUrl(string? entry)
    {
        if (!string.IsNullOrWhiteSpace(entry))
        {
            entry = entry.Replace('.', '/');
                
            entry = entry[0] == '/' ? 
                entry.Substring(1) : 
                entry;

            entry = entry[(entry.Length - 1)] == '/'
                ? entry.Substring(0, (entry.Length - 1))
                : entry;
        }

        return entry;
    }
}