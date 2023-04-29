using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Attributes;
using Tisa.Store.Web.Infrastructures.Contracts.Database;

namespace Tisa.Store.Web.Data.Seeds;

[Order(0)]
public class TypeSeed : ISeed<ApplicationContext>
{
    public Task<Models.Entities.Type[]> Get()
    {
        return Task.FromResult(new Models.Entities.Type[]
        {
            new Models.Entities.Type()
            {
                Name = nameof(Int32)
            },
            new Models.Entities.Type()
            {
                Name = nameof(Single)
            },
            new Models.Entities.Type()
            {
                Name = nameof(Byte)
            },
            new Models.Entities.Type()
            {
                Name = nameof(Boolean)
            },
            new Models.Entities.Type()
            {
                Name = nameof(String)
            },
            new Models.Entities.Type()
            {
                Name = nameof(Char)
            }
        });
    }

    public async Task Invoke(ApplicationContext context)
    {
        if (!await context.Types.AnyAsync())
        {
            await context.Types.AddRangeAsync(await Get());
            await context.SaveChangesAsync();
        }
    }
}