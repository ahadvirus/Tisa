using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Attributes;
using Tisa.Store.Web.Infrastructures.Contracts.Database;

namespace Tisa.Store.Web.Data.Seeds;

[Order(1)]
public class AttributeSeed : ISeed<ApplicationContext>
{

    public async Task Invoke(ApplicationContext context)
    {
        if (!await context.Attributes.AnyAsync())
        {
            int typeId = await context.Types
                .Where(type => type.Name == nameof(Int32))
                .Select(type => type.Id)
                .FirstOrDefaultAsync();

            if (typeId != 0)
            {

                await context.Attributes.AddAsync(new Models.Entities.Attribute()
                {
                    Name = nameof(Models.Entities.Attribute.Id),
                    Description = "Primary key in entity",
                    TypeId = typeId
                });

                await context.SaveChangesAsync();
            }
        }

    }
}