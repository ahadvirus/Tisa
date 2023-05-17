using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts;

public interface IRepository<in TPrimary, TResult> : IRepository where TPrimary : struct
{
    Task<IEnumerable<TResult>> Get(Func<TResult, bool>? predicate = null);
    Task<TResult> Get(TPrimary primary);
    Task<TResult> Add(TResult entry);
    Task<TResult> Update(TResult entry);
    Task<bool> Delete(TResult entry);
    Task<bool> Delete(TPrimary primary);
}