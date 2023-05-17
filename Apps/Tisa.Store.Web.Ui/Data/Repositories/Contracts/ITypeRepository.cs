using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Models.DataTransfers;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts;

public interface ITypeRepository : IRepository<int, TypeDto>
{
    Task<bool> Exist(int id);
    Task<bool> Valid(int id, int type);
}