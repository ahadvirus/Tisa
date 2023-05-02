using System.Collections.Generic;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Products;

public class ProductDTO : IProductDTO
{
    public ProductDTO()
    {
        Name = string.Empty;
        Properties = new List<PropertyDTO>();
    }
    
    public string Name { get; set; }
    public IEnumerable<IPropertyDTO> Properties { get; set; }
}