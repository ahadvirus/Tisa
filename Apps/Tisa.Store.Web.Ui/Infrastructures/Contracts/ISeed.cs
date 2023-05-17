using System.Threading.Tasks;

namespace Tisa.Store.Web.Ui.Infrastructures.Contracts;

public interface ISeed<in TRepository> where TRepository : IRepository
{
    Task Invoke(TRepository repository);
}