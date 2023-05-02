using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Products;

public class RequestVM
{
    public RequestVM()
    {
        Entity = string.Empty;
    }
    
    [FromRoute]
    [JsonIgnore]
    public string Entity { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int EntityId { get; set; }
}