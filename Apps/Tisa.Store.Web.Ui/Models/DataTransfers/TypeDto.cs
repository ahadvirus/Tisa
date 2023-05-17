using System.ComponentModel.DataAnnotations;

namespace Tisa.Store.Web.Ui.Models.DataTransfers;

public record TypeDto
{
    [Display(Name = nameof(Id))]
    public int Id { get; init; }
    [Display(Name = nameof(Display))]
    public string Display { get; init; }
    public string Name { get; init; }
    public int TypeId { get; init; }
}