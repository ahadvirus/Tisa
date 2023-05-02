using System.Collections.Generic;

namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IProductDTO
{
    string Name { get; set; }
    IEnumerable<IPropertyDTO> Properties { get; set; }
}