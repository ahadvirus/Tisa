using System.Text.Json.Serialization;

namespace Tisa.Store.Web.Ui.Models.DataTransfers.Api;

public record TypeDto
{
    [JsonConstructor]
    public TypeDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }
}