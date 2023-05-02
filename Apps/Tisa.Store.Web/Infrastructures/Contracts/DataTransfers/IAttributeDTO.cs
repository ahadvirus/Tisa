namespace Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

public interface IAttributeDTO
{
    int Id { get; set; }
    string Name { get; set; }
    string Type { get; set; }
}