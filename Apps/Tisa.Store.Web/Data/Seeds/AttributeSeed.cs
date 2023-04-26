using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Contracts;
using Tisa.Store.Web.Infrastructures.Attributes;

namespace Tisa.Store.Web.Data.Seeds;

[Order(1)]
public class AttributeSeed : ISeed<ApplicationContext>
{

    public async Task Invoke(ApplicationContext context)
    {
        int typeId = await context.Types
            .Where(type => type.Kind == nameof(Int32))
            .Select(type => type.Id)
            .FirstOrDefaultAsync();

        if (typeId != 0)
        {

            await context.Attributes.AddAsync(new Models.Entities.Attribute()
            {
                Name = nameof(Models.Entities.Attribute.Id),
                Title = nameof(Models.Entities.Attribute.Id),
                TypeId = typeId
            });

            await context.SaveChangesAsync();
        }

    }
}