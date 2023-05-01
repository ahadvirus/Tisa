using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Models.ViewModels.Validators;

namespace Tisa.Store.Web.Controllers.Validator;

[ApiController]
[Route(
    template: "[namespace]",
    Name = "[namespace].[controller]"
)]
public class IndexController : ControllerBase
{
    private ApplicationContext Context { get; }

    private IMapper Mapper { get; }

    public IndexController(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<IndexVM>>> Invoke(CancellationToken cancellationToken)
    {
        return Ok(
            await Context.Validators.ProjectTo<IndexVM>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        );
    }
}