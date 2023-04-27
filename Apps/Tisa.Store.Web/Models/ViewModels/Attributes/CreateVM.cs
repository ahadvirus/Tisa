using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Attributes;

[DisplayName(nameof(Attribute) + nameof(CreateVM))]
public class CreateVM
{
    public CreateVM()
    {
        Title = string.Empty;
        Display = string.Empty;
        Type = string.Empty;
    }

    [Required]
    public string Title { get; set; }
    [Required]
    public string Display { get; set; }
    [Required]
    public string Type { get; set; }
}