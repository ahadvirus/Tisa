using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes;

public class DeleteVM
{
    [FromRoute]
    [JsonIgnore]
    public string Entity { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int EntityId { get; set; }

    [FromRoute]
    [JsonIgnore]
    public string Attribute { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int AttributeId { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int AttributeEntityId { get; set; }
}