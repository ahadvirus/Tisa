using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Validators;

namespace Tisa.Store.Web.Controllers.Entity.Attribute.Validator;

[ApiController]
[Route(
    template: nameof(Models.Entities.Entity) +
              "/{" +
              nameof(Models.ViewModels.Entities.Attributes.Validators.RequestVM.Entity) +
              ":" +
              nameof(Models.Entities.Entity) +
              "}/" +
              nameof(Models.Entities.Attribute) +
              "/{" +
              nameof(Models.ViewModels.Entities.Attributes.Validators.RequestVM.Attribute) +
              ":" +
              nameof(Models.Entities.AttributeEntity) +
              "}/" +
              nameof(Models.Entities.Validator),
    Name = "[namespace].[controller]"
)]
public class CreateController : ControllerBase
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public CreateController(ApplicationContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Invoke(
        [FromRoute] Models.ViewModels.Entities.Attributes.Validators.RequestVM request,
        [FromBody] Models.ViewModels.Entities.Attributes.Validators.CreateVM entry,
        CancellationToken cancellationToken
    )
    {
        //Checking send valid validator

        Models.DataTransfers.Entities.Attributes.Validators.ValidatorDTO? validatorDto = await Context.Validators
            .Where(validator => validator.Name == entry.Name)
            .Include(validator => validator.Types)
            .Include(validator => validator.Claims)
            .ProjectTo<Models.DataTransfers.Entities.Attributes.Validators.ValidatorDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (validatorDto == null)
        {
            ModelState.AddModelError(
                nameof(Models.ViewModels.Entities.Attributes.Validators.CreateVM.Name),
                string.Format("Please send valida validator, we couldn't find `{0}` validator.",
                    entry.Name)
            );

            return BadRequest(ModelState);
        }

        // Attribute type must be acceptable by validator

        Models.DataTransfers.Entities.Attributes.Validators.AttributeDTO attributeDto = await Context.Attributes
            .Where(attribute => attribute.Id == request.AttributeId)
            .Include(attribute => attribute.Type)
            .ProjectTo<Models.DataTransfers.Entities.Attributes.Validators.AttributeDTO>(Mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        if (validatorDto.Types.All(type => type != attributeDto.TypeId))
        {
            ModelState.AddModelError(
                nameof(Models.Entities.Validator),
                string.Format("The `{0}` validator can't support {1} type of value.",
                    entry.Name,
                    attributeDto.Type)
            );

            return BadRequest(ModelState);
        }

        // Checking send valid parameters for validator
        if (validatorDto.ParametersValidator == null)
        {
            ModelState.AddModelError(
                entry.Name,
                string.Format(
                    "Something bad happened, please tell the developer for checking the `{0}` validators parameter checker",
                    entry.Name
                ));
            return BadRequest(ModelState);
        }

        ValidateParameter validate = new ValidateParameter(
            attributeDto,
            await Context.Entities.Where(entity => entity.Id == request.EntityId)
                .Include(entity => entity.Attributes)
                .ThenInclude(attribute => attribute.Attribute)
                .SelectMany(entity => entity.Attributes)
                .Select(attribute => attribute.Attribute)
                .ProjectTo<Models.DataTransfers.Entities.Attributes.Validators.AttributeDTO>(
                    Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            validatorDto.ParametersValidator
        );

        if (!await validate.IsValid(entry.Parameters))
        {
            ModelState.AddModelError(
                nameof(entry.Parameters),
                string.Format(
                    "The `{0}` validator couldn't accept the `{1}` as parameters, please send valid parameters",
                    entry.Name,
                    entry.Parameters
                ));
            return BadRequest(ModelState);
        }

        // Save validator for attribute entity

        Models.Entities.AttributeEntityValidator entity = new Models.Entities.AttributeEntityValidator()
        {
            AttributeEntityId = request.AttributeEntityId,
            ValidatorId = validatorDto.Id,
            Claims = new List<Models.Entities.AttributeEntityValidatorClaim>()
            {
                new Models.Entities.AttributeEntityValidatorClaim()
                {
                    Key = nameof(entry.Parameters),
                    Value = entry.Parameters
                }
            }
        };

        await Context.AttributeEntityValidators.AddAsync(entity, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return Ok(new Models.ViewModels.Entities.Attributes.Validators.IndexVM()
        {
            Id = entity.Id,
            Name = entry.Name,
            Description = validatorDto.Description,
            Parameters = entry.Parameters
        });
    }
}