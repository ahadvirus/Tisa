using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;

namespace Tisa.Store.Web.Ui.Models.DataTransfers;

public record TypeDto
{
    [Display(Name = nameof(Id))]
    public int Id { get; init; }
    [Display(Name = nameof(Display))]
    [Required(ErrorMessage = nameof(Display) + nameof(DataAnnotations.Required) + nameof(RequiredAttribute.ErrorMessage))]
    public string Display { get; init; }
    public string Name { get; init; }
    public int TypeId { get; init; }
}