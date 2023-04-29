using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes;

public class CreateVM
{
    public CreateVM()
    {
        Name = string.Empty;
    }
    
    [FromBody]
    public string Name { get; set; }
}