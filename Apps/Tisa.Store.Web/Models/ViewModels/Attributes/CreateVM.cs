using System.ComponentModel.DataAnnotations;

namespace Tisa.Store.Web.Models.ViewModels.Attributes;

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