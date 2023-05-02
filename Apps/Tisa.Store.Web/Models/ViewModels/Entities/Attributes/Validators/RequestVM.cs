using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes.Validators;

public class RequestVM : Models.ViewModels.Entities.Attributes.RequestVM
{
    public RequestVM()
    {
        Entity = string.Empty;
        Attribute = string.Empty;
        Validator = string.Empty;
    }
    
    [FromRoute]
    [JsonIgnore]
    public string Validator { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int ValidatorId { get; set; }
    
    [FromHeader]
    [JsonIgnore]
    public int AttributeEntityValidatorId { get; set; }
}