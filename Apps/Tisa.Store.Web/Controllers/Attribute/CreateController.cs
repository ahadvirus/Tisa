using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;

namespace Tisa.Store.Web.Controllers.Attribute;

[ApiController]
[Route(
    template: "[namespace]",
    Name = "[namespace]"
)]
public class CreateController : ControllerBase
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private InlineValidator<Models.ViewModels.Attributes.CreateVM> Validator { get; }

    public CreateController(ApplicationContext context, IMapper mapper)
    {
        Validator = new InlineValidator<Models.ViewModels.Attributes.CreateVM>()
        {
            validator => validator.RuleFor(expression: vm => vm.Title)
                .NotNull(),
            validator => validator.RuleFor(expression: vm => vm.Description)
                .NotNull(),
            validator => validator.RuleFor(expression: vm => vm.Type)
                .NotNull()
        };
        
        Context = context;
        Mapper = mapper;
    }
    
    // GET
    [HttpPost]
    public async Task<ActionResult<Models.ViewModels.Attributes.IndexVM>> Invoke(
        [FromBody]Models.ViewModels.Attributes.CreateVM entry,
        CancellationToken cancellation
    )
    {
        ValidationResult validation = await Validator.ValidateAsync(instance: entry, cancellation: cancellation);

        if (!validation.IsValid)
        {
            foreach (ValidationFailure failure in validation.Errors)
            {
                ModelState.AddModelError(
                    key: failure.PropertyName,
                    errorMessage: failure.ErrorMessage
                    );
            }

            return BadRequest(ModelState);
        }
        
        Models.Entities.Type? type = await Context.Types.Where(type => type.Name == entry.Type)
            .FirstOrDefaultAsync(cancellation);

        if (type == null)
        {
            ModelState.AddModelError(
                nameof(Models.ViewModels.Attributes.CreateVM.Type),  
                string.Format("Please send valid `{0}`", nameof(Models.ViewModels.Attributes.CreateVM.Type))
            );

            return BadRequest(ModelState);
        }

        Models.Entities.Attribute attribute = Mapper.Map<Models.Entities.Attribute>(entry);
        
        attribute.TypeId = type.Id;

        await Context.Attributes.AddAsync(attribute, cancellation);

        await Context.SaveChangesAsync(cancellation);

        attribute.Type = type;
        
        return Ok(Mapper.Map<Models.ViewModels.Attributes.IndexVM>(attribute));

    }
}