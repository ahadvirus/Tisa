using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;
using Tisa.Store.Web.Infrastructures.Validators;
using Tisa.Store.Web.Models.DataTransfers.Products.Entities;
using Tisa.Store.Web.Models.ViewModels.Products;

namespace Tisa.Store.Web.Controllers.Product.Entity;

[ApiController]
[Route(
    template: nameof(Models.Entities.Product) +
              "/{" +
              nameof(RequestVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}",
    Name = "[namespace].[controller]"
)]
public class CreateController : ControllerBase
{
    private ApplicationContext Context { get; }

    private IMapper Mapper { get; }

    private string Key
    {
        get
        {
            return "Id";
        }
    }

    private ValidatorFactory ValidatorFactory { get; }

    public CreateController(ApplicationContext context, IMapper mapper, ValidatorFactory validatorFactory)
    {
        Context = context;
        Mapper = mapper;
        ValidatorFactory = validatorFactory;
    }

    [HttpPost]
    public async Task<ActionResult> Invoke(
        [FromRoute] RequestVM request,
        [FromBody] dynamic entry,
        CancellationToken cancellationToken
    )
    {
        // Checking send valid data type

        ObjectTypeDTO objectType = new ObjectTypeDTO(entry);

        if (!objectType.IsValid)
        {
            return BadRequest();
        }

        // Fetching attributes

        IEnumerable<IAttributeEntityDTO> attributes = await Context.AttributeEntities
            .Where(attribute => attribute.EntityId == request.EntityId)
            .ProjectTo<IAttributeEntityDTO>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // Converting data to product

        IDictionary<string, object?> values = (
                new CreateVM(
                    Mapper.Map<IEnumerable<IAttributeEntityDTO>, IEnumerable<IAttributeDTO>>(attributes.Where(attribute => attribute.Name != Key)),
                    (JsonElement)entry
                )
            )
            .GetValues();

        // Create dynamic class

        object? entity = (new Infrastructures.Dynamic.ClassBuilder(request.Entity)).CreateObject(
            attributes
                .Where(attribute => attribute.Name == Key)
                .ToDictionary(attribute => attribute.Name, attribute => attribute.GetType)
        );

        if (entity == null)
        {
            return BadRequest();
        }

        foreach (PropertyInfo property in entity.GetType().GetProperties())
        {
            if (values.TryGetValue(property.Name, out object? value) && value != null)
            {
                property.SetValue(entity, value);
            }
        }

        // Make validator for product

        Infrastructures.Contracts.Validator.IValidator? validator =
            ValidatorFactory.CreateInstance(entity.GetType(), attributes);

        if (validator == null)
        {
            return BadRequest();
        }

        if (!await validator.Validate(entity))
        {
            foreach (KeyValuePair<string, List<string>> error in await validator.GetErrors())
            {
                foreach (string message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }

            return BadRequest(ModelState);
        }
        
        

        return Ok(entry.GetType());
    }
}