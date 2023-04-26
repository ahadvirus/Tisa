using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tisa.Store.Web.Infrastructures.Contracts;

public interface ISeed<T> where T : DbContext
{
    Task Invoke(T context);
}