using Tisa.Store.Web.Infrastructures.Contracts.Claims.AttributeEntityValidator;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes.Validators;

public class CreateVM : IParameter
{
    public CreateVM()
    {
        Name = string.Empty;
        Parameters = string.Empty;
    }
    
    public string Name { get; set; }
    public string Parameters { get; set; }
}