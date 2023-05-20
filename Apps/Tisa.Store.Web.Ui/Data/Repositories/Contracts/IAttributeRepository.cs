using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts;

public interface IAttributeRepository : IRepository
{
    /// <summary>
    /// Get all exist attributes
    /// </summary>
    /// <param name="predicate"> Customize result before return</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    Task<IEnumerable<Models.DataTransfers.AttributeDto>> GetAsync(Func<Models.DataTransfers.AttributeDto, bool>? predicate = null);

    /// <summary>
    /// Get one of attribute if exist in database otherwise null
    /// </summary>
    /// <param name="id"><see cref="int"/> Primary key of specific attribute</param>
    /// <returns><see cref="Nullable{T}"/> </returns>
    Task<Models.DataTransfers.AttributeDto?> GetAsync(int id);

    /// <summary>
    /// Add new attribute to system (on both side local and api)
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.AttributeDto"/> Th attribute you want to implement system</param>
    /// <returns><see cref="Models.DataTransfers.AttributeDto"/> The entry with primary key after finished</returns>
    /// <exception cref="System.Exception">Throw when the data send to api is wrong and message contains with api response</exception>
    Task<Models.DataTransfers.AttributeDto> AddAsync(Models.DataTransfers.AttributeDto entry);

    /// <summary>
    /// Add new attribute to local database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.AttributeDto"/> Th attribute you want to implement system</param>
    /// <returns><see cref="Models.DataTransfers.AttributeDto"/> The entry with primary key after finished</returns>
    /// <exception cref="System.Exception">Throw when the data was saved before</exception>
    Task<Models.DataTransfers.AttributeDto> SaveAsync(Models.DataTransfers.AttributeDto entry);

    /// <summary>
    /// Update attribute in local database
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.AttributeDto"/> the attribute you want to update</param>
    /// <returns><see cref="Models.DataTransfers.AttributeDto"/> Return with all data have been save</returns>
    /// <exception cref="System.Exception">Throw when the data send to api is wrong and message contains with api response</exception>
    Task<Models.DataTransfers.AttributeDto> UpdateAsync(Models.DataTransfers.AttributeDto entry);

    /// <summary>
    /// Delete attribute in both side
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.AttributeDto"/> the attribute you want to update</param>
    /// <returns><see cref="bool"/> Return <c>true</c> if action done else <c>false</c></returns>
    Task<bool> DeleteAsync(Models.DataTransfers.AttributeDto entry);

    /// <summary>
    /// Delete attribute in both side
    /// </summary>
    /// <param name="id"><see cref="int"/> Primary key of attribute want it to delete</param>
    /// <returns><see cref="bool"/> Return <c>true</c> if action done else <c>false</c></returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Checking the attribute exist
    /// </summary>
    /// <param name="id"><see cref="int"/> Primary key of attribute want it to check it</param>
    /// <returns><see cref="bool"/> Return <c>true</c> if attribute exist else <c>false</c></returns>
    Task<bool> ExistAsync(int id);

    /// <summary>
    /// Checking the attribute of api connected to specific attribute in local database
    /// </summary>
    /// <param name="id"><see cref="int"/> Primary key of attribute want it to check it</param>
    /// <param name="attribute"><see cref="int"/> Primary key of api attribute want it to check it</param>
    /// <returns><see cref="bool"/> Return <c>true</c> if attribute exist and accurately connected to api attribute else <c>false</c></returns>
    Task<bool> ValidAsync(int id, int attribute);
}