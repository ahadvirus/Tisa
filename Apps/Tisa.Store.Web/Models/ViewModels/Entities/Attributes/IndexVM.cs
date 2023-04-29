using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes;

[DisplayName(nameof(Models.Entities.Entity) + nameof(Models.Entities.Attribute) + nameof(IndexVM))]
public class IndexVM
{
    public IndexVM()
    {
        Entity = string.Empty;
    }
    [FromRoute(Name = nameof(Entity))]
    [JsonIgnore]
    public string Entity { get; set; }
    
    [FromHeader(Name = nameof(Models.Entities.Entity) + nameof(Models.Entities.Entity.Id))]
    [JsonIgnore]
    public int EntityId { get; set; }
}