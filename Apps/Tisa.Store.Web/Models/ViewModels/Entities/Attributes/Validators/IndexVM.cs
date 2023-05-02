using Tisa.Store.Web.Infrastructures.Contracts.Claims.AttributeEntityValidator;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes.Validators;

public class IndexVM : IParameter
{
    public IndexVM()
    {
        Name = string.Empty;
        Description = string.Empty;
        Parameters = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Parameters { get; set; }
}