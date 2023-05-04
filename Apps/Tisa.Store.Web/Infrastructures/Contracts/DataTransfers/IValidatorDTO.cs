namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IValidatorDTO : Claims.Validators.IBuilder, Claims.Validators.IParameter
{
    string Name { get; set; }
}