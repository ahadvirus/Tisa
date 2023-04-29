using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tisa.Store.Web.Infrastructures.Contracts.Database;

public interface ISeed<T> where T : DbContext
{
    Task Invoke(T context);
}