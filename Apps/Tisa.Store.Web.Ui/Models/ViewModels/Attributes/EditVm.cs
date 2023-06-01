using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;

namespace Tisa.Store.Web.Ui.Models.ViewModels.Attributes;

public record EditVm
{
    public EditVm()
    {
        Display = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// The key of attribute
    /// </summary>
    [Display(Name = nameof(Id))]
    public int Id { get; init; }

    /// <summary>
    /// The name of attribute for translation
    /// </summary>
    [Display(Name = nameof(Display))]
    [Required(ErrorMessage = nameof(Display) + nameof(DataAnnotations.Required) + nameof(RequiredAttribute.ErrorMessage))]
    public string Display { get; init; }

    /// <summary>
    /// The description of attribute for translation
    /// </summary>
    [Display(Name = nameof(Description))]
    [Required(ErrorMessage = nameof(Description) + nameof(DataAnnotations.Required) + nameof(RequiredAttribute.ErrorMessage))]
    public string Description { get; init; }
}