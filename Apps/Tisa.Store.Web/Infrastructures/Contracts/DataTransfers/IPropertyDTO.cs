namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IPropertyDTO
{
    string Name { get; set; }
    string Value { get; set; }
    string Type { get; set; }
}