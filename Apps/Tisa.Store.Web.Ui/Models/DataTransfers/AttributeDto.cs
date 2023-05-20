namespace Tisa.Store.Web.Ui.Models.DataTransfers;

public record AttributeDto
{
    public AttributeDto()
    {
        Display = string.Empty;
        Description = string.Empty;
        Type = string.Empty;
    }

    public int Id { get; init; }
    public int AttributeId { get; init; }
    public string Display { get; init; }
    public string Description { get; init; }
    public string Type { get; init; }
    public int TypeId { get; init; }
}