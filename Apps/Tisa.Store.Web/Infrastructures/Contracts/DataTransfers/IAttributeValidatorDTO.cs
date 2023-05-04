using System.Collections.Generic;

namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IAttributeValidatorDTO : IValidatorDTO, Claims.AttributeEntityValidator.IParameter 
{
}