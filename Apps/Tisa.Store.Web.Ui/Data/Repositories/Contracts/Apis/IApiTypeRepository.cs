using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Models.DataTransfers.Api;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

public interface IApiTypeRepository : IApiRepository
{
    /// <summary>
    /// Return all type exist in api
    /// </summary>
    /// <param name="predicate"><see cref="Func{T, TResult}"/> Predicate before get list</param>
    /// <returns></returns>
    Task<IEnumerable<TypeDto>> GetASync(Func<TypeDto, bool>?  predicate = null);

    /// <summary>
    /// Return specific type exist in api
    /// </summary>
    /// <param name="id"><see cref="int"/> The type you trying to get it</param>
    /// <returns><see cref="Nullable{TypeDto}"/>Return null if send wrong id</returns>
    Task<TypeDto?> GetAsync(int  id);
}