using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
                .NotEmpty(),
            validator => validator.RuleFor(expression: vm => vm.Description)
                .NotEmpty(),
            validator => validator.RuleFor(expression: vm => vm.Type)
                .NotEmpty()
        };

        Context = context;
        Mapper = mapper;
    }

    // GET
    [HttpPost]
    public async Task<ActionResult<Models.ViewModels.Attributes.IndexVM>> Invoke(
        [Bind(include: new string[]{nameof(Models.ViewModels.Attributes.CreateVM.Title), nameof(Models.ViewModels.Attributes.CreateVM.Description), nameof(Models.ViewModels.Attributes.CreateVM.Type)})] Models.ViewModels.Attributes.CreateVM entry,
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

        Expression<Func<Models.Entities.Type, bool>> predicate = Regex.IsMatch(input: entry.Type, pattern: "^\\d+$")
            ? type => type.Id == int.Parse(entry.Type)
            : type => type.Name == entry.Type;

        Models.Entities.Type? type = await Context.Types.Where(predicate: predicate)
            .FirstOrDefaultAsync(cancellation);

        if (type == null)
        {
            ModelState.AddModelError(
                key: nameof(Models.ViewModels.Attributes.CreateVM.Type),
                errorMessage: string.Format(
                    format: "Please send valid `{0}`",
                    args: new object[] { nameof(Models.ViewModels.Attributes.CreateVM.Type) }
                )
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