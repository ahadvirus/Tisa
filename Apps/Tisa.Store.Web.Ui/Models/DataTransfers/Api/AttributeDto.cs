using System.Text.Json.Serialization;

namespace Tisa.Store.Web.Ui.Models.DataTransfers.Api;

public record AttributeDto
{
    [JsonConstructor]
    public AttributeDto(int id, string title, string description, string type)
    {
        Id = id;
        Title = title;
        Description = description;
        Type = type;
    }

    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Type { get; init; }
}