using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

public interface IApiAttributeRepository : IApiRepository
{
    /// <summary>
    /// Return all attributes exist in api
    /// </summary>
    /// <param name="predicate">Predicate to customize result before return</param>
    /// <returns><see cref="IEnumerable{T}"/></returns>
    Task<IEnumerable<Models.DataTransfers.Api.AttributeDto>> GetAsync(Func<Models.DataTransfers.Api.AttributeDto, bool>? predicate = null);

    /// <summary>
    /// Return only one specific attribute exist in api
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Models.DataTransfers.Api.AttributeDto?> GetAsync(int id);

    /// <summary>
    /// Add new attribute to api side
    /// </summary>
    /// <param name="entry"><see cref="Models.DataTransfers.Api.AttributeDto"/>The new attribute try to add</param>
    /// <returns><see cref="Models.DataTransfers.Api.AttributeDto"/> Return the attribute with the Id in api side</returns>
    Task<Models.DataTransfers.Api.AttributeDto> AddAsync(Models.DataTransfers.Api.AttributeDto entry);

    /// <summary>
    /// Delete attribute in api side
    /// </summary>
    /// <param name="id"><see cref="int"/> The id of attribute want to delete</param>
    /// <returns><see cref="bool"/> Return result of action as <c>true</c> or <c>false</c></returns>
    Task<bool> DeleteAsync(int id);

}