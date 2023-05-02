using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes.Validators;

public class RequestVM : DeleteVM
{
    public RequestVM()
    {
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