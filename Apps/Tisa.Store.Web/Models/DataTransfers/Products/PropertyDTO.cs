using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Products;

public class PropertyDTO : IPropertyDTO
{
    public PropertyDTO()
    {
        Name = string.Empty;
        Value = string.Empty;
        Type = string.Empty;
    }
    
    public string Name { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}