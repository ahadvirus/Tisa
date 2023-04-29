using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes;

public class DeleteVM : IndexVM
{
    public DeleteVM()
    {
        Entity = string.Empty;
        Attribute = string.Empty;
    }

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