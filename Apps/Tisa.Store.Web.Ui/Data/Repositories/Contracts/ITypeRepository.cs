using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts;

public interface ITypeRepository : IRepository
{
    /// <summary>
    /// Get all types exist in api and local database
    /// </summary>
    /// <param name="predicate">For customize list before return</param>
    /// <returns></returns>
    Task<IEnumerable<Models.DataTransfers.TypeDto>> GetAsync(Func<Models.DataTransfers.TypeDto, bool>? predicate = null);

    /// <summary>
    /// Fetching only one Type from database
    /// </summary>
    /// <param name="primary"><see cref="int"/>Primary key in database</param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">Happened when send wrong Id (Primary key)</exception>
    Task<Models.DataTransfers.TypeDto> GetAsync(int primary);

    /// <summary>
    /// Adding new Type to database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.TypeDto"/></param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">When throw the exception send invalid TypeId or exist TypeId</exception>
    Task<Models.DataTransfers.TypeDto> AddAsync(Models.DataTransfers.TypeDto entry);

    /// <summary>
    /// Update translation of type in local database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.TypeDto"/></param>
    /// <returns><see cref="Models.DataTransfers.TypeDto"/></returns>
    /// <exception cref="System.Exception">When throw the entry is invalid in both side</exception>
    Task<Models.DataTransfers.TypeDto> UpdateAsync(Models.DataTransfers.TypeDto entry);

    /// <summary>
    /// Checking type exist in both side
    /// </summary>
    /// <param name="id"><see cref="int"/>Primary key of type in local database</param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> ExistAsync(int id);

    /// <summary>
    /// Checking type to point accurate type in local database
    /// </summary>
    /// <param name="id"><see cref="int"/>Primary key of type in local database</param>
    /// <param name="type"><see cref="int"/>Index key of type in local database</param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> ValidAsync(int id, int type);
}