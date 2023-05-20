using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Ui.Infrastructures.Attributes;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Attributes;

public record CreateVm
{
    public CreateVm()
    {
        Display = string.Empty;
        Description = string.Empty;
    }

    [Display(Name = nameof(Display))]
    [Required(ErrorMessage = nameof(Display) + nameof(DataAnnotations.Required) + nameof(RequiredAttribute.ErrorMessage))]
    [RegularExpression(pattern: "^[A-Za-z0-9]+$", ErrorMessage = nameof(Display) + nameof(DataAnnotations.RegularExpression) + nameof(RegularExpressionAttribute.ErrorMessage))]
    [PascalName(ErrorMessage = nameof(Display) + nameof(DataAnnotations.PascalName) + nameof(PascalNameAttribute.ErrorMessage))]
    public string Display { get; init; }

    [Display(Name = nameof(Description))]
    [Required(ErrorMessage = nameof(Description) + nameof(DataAnnotations.Required) + nameof(RequiredAttribute.ErrorMessage))]
    [RegularExpression(pattern: "^[A-Za-z0-9\\s\\'\\\"\\,\\(\\)\\[\\]\\\\_\\-]+$", ErrorMessage = nameof(Display) + nameof(DataAnnotations.RegularExpression) + nameof(RegularExpressionAttribute.ErrorMessage))]
    public string Description { get; init; }
    

    public int Type { get; set; }
}