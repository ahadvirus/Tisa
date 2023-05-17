using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tisa.Store.Web.Ui.Models.DataTransfers.Api;

namespace Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;

public interface IApiTypeRepository
{
    Task<IEnumerable<TypeDto>> Get(Func<TypeDto, bool>?  predicate = null);
    Task<TypeDto?> Get(int  id);
}