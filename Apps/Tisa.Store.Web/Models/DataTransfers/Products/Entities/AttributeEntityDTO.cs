using System.Collections.Generic;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Products.Entities;

public class AttributeEntityDTO : AttributeDTO, IAttributeEntityDTO 
{
    public IEnumerable<IAttributeValidatorDTO> Validators { get; set; }
}