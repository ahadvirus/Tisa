using System;
using System.Threading.Tasks;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Contracts;
using Tisa.Store.Web.Infrastructures.Attributes;

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
                Kind = nameof(Int32)
            },
            new Models.Entities.Type()
            {
                Kind = nameof(Single)
            },
            new Models.Entities.Type()
            {
                Kind = nameof(Byte)
            },
            new Models.Entities.Type()
            {
                Kind = nameof(Boolean)
            },
            new Models.Entities.Type()
            {
                Kind = nameof(String)
            },
            new Models.Entities.Type()
            {
                Kind = nameof(Char)
            }
        });
    }

    public async Task Invoke(ApplicationContext context)
    {
        await context.Types.AddRangeAsync(await Get());
        await context.SaveChangesAsync();
    }
}