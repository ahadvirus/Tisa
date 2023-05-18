namespace Tisa.Store.Web.Ui.Models.DataTransfers;

public record AttributeDto()
{
    public int Id { get; init; }
    public int AttributeId { get; init; }
    public string Display { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}