using System.ComponentModel;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Attributes;

[DisplayName(nameof(Attribute) + nameof(CreateVM))]
public class CreateVM
{
    public CreateVM()
    {
        Title = string.Empty;
        Description = string.Empty;
        Type = string.Empty;
    }

    
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}