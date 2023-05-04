using System.Collections.Generic;
using Tisa.Store.Web.Models.DataTransfers.Products.Entities;

namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IAttributeEntityDTO : IAttributeDTO
{
    IEnumerable<IAttributeValidatorDTO> Validators { get; set; }
}