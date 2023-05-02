using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Attributes;
using Tisa.Store.Web.Infrastructures.Contracts.Database;
using Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Validators;
using Tisa.Store.Web.Infrastructures.Validators.Builders;
using Tisa.Store.Web.Infrastructures.Validators.Checkers;
using Tisa.Store.Web.Models.Entities;
using Validator = Tisa.Store.Web.Models.Entities.Validator;

namespace Tisa.Store.Web.Data.Seeds;

[Order(2)]
public class ValidatorSeed : ISeed<ApplicationContext>
{
    private Task<Models.ViewModels.Validators.CreateVM[]> Get()
    {
        return Task.FromResult(new Models.ViewModels.Validators.CreateVM[]
        {
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "NotNull",
                Description = "Ensures that the specified property is not null.",
                NeedParameters = false,
                ParametersValidator = new EmptyParameterValidator(),
                ValidatorBuilder = new NotNullValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "NotEmpty",
                Description =
                    "Ensures that the specified property is not null, an empty string or whitespace (or the default value for value types)",
                NeedParameters = false,
                ParametersValidator = new EmptyParameterValidator(),
                ValidatorBuilder = new NotEmptyValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "NotEqual",
                Description =
                    "Ensures that the value of the specified property is not equal to a particular value (or not equal to the value of another property)",
                NeedParameters = true,
                ParametersValidator = new SingleParameterValidator(),
                ValidatorBuilder = new NotEqualValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "Equal",
                Description =
                    "Ensures that the value of the specified property is equal to a particular value (or equal to the value of another property)",
                NeedParameters = true,
                ParametersValidator = new SingleParameterValidator(),
                ValidatorBuilder = new EqualValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "Length",
                Description =
                    "Ensures that the length of a particular string property is within the specified range. However, it doesn’t ensure that the string property isn’t null.",
                NeedParameters = true,
                ParametersValidator = new LengthParameterValidator(),
                ValidatorBuilder = new LengthValidatorBuilder(),
                Types = new string[]
                {
                    nameof(String),
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "MaximumLength",
                Description =
                    "Ensures that the length of a particular string property is no longer than the specified value.",
                NeedParameters = true,
                ParametersValidator = new StringOperatorParameterValidator(),
                ValidatorBuilder = new MaximumLengthValidatorBuilder(),
                Types = new string[]
                {
                    nameof(String),
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "MinimumLength",
                Description =
                    "Ensures that the length of a particular string property is longer than the specified value.",
                NeedParameters = true,
                ParametersValidator = new StringOperatorParameterValidator(),
                ValidatorBuilder = new MinimumLengthValidatorBuilder(),
                Types = new string[]
                {
                    nameof(String),
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "LessThan",
                Description =
                    "Ensures that the value of the specified property is less than a particular value (or less than the value of another property)",
                NeedParameters = true,
                ParametersValidator = new MathematicsParameterValidator(),
                ValidatorBuilder = new LessThanValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "LessThanOrEqualTo",
                Description =
                    "Ensures that the value of the specified property is less than or equal to a particular value (or less than or equal to the value of another property)",
                NeedParameters = true,
                ParametersValidator = new MathematicsParameterValidator(),
                ValidatorBuilder = new LessThanOrEqualToValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "GreaterThan",
                Description =
                    "Ensures that the value of the specified property is greater than a particular value (or greater than the value of another property)",
                NeedParameters = true,
                ParametersValidator = new MathematicsParameterValidator(),
                ValidatorBuilder = new GreaterThanValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "GreaterThanOrEqualTo",
                Description =
                    "Ensures that the value of the specified property is greater than or equal to a particular value (or greater than or equal to the value of another property)",
                NeedParameters = true,
                ParametersValidator = new MathematicsParameterValidator(),
                ValidatorBuilder = new GreaterThanOrEqualToValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "Matches",
                Description =
                    "Ensures that the value of the specified property matches the given regular expression.",
                NeedParameters = true,
                ParametersValidator = new SingleParameterValidator(),
                ValidatorBuilder = new MatchesValidatorBuilder(),
                Types = new string[]
                {
                    nameof(String)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "Empty",
                Description =
                    "Ensures that the specified property value is null, or is the default value for the type",
                NeedParameters = false,
                ParametersValidator = new EmptyParameterValidator(),
                ValidatorBuilder = new EmptyValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            },
            new Models.ViewModels.Validators.CreateVM()
            {
                Name = "Null",
                Description =
                    "Ensures that the specified property value is null",
                NeedParameters = false,
                ParametersValidator = new EmptyParameterValidator(),
                ValidatorBuilder = new NullValidatorBuilder(),
                Types = new string[]
                {
                    nameof(Int32),
                    nameof(Single),
                    nameof(Byte),
                    nameof(Boolean),
                    nameof(String),
                    nameof(Char)
                }
            }
        });
    }

    public async Task Invoke(ApplicationContext context)
    {
        if (!await context.Validators.AnyAsync())
        {
            IDictionary<string, int> types = new Dictionary<string, int>()
            {
                {
                    nameof(Int32),
                    0
                },
                {
                    nameof(Single),
                    0
                },
                {
                    nameof(Byte),
                    0
                },
                {
                    nameof(Boolean),
                    0
                },
                {
                    nameof(String),
                    0
                },
                {
                    nameof(Char),
                    0
                }
            };

            IConfigurationProvider configuration = new MapperConfiguration(configure =>
                configure.AddProfile<CreateProfile>());

            IMapper mapper = new Mapper(configuration);

            foreach (string entry in types.Keys)
            {
                types[entry] = await context.Types
                    .Where(type => type.Name == entry)
                    .Select(type => type.Id)
                    .FirstOrDefaultAsync();
            }

            foreach (Models.ViewModels.Validators.CreateVM item in await Get())
            {
                Validator entity = mapper.Map<Models.ViewModels.Validators.CreateVM, Validator>(item);
                
                entity.Types = item.Types
                    .Select(type => types[type])
                    .Where(type => type != 0)
                    .Select(type => new TypeValidator() { TypeId = type })
                    .ToList();

                await context.Validators.AddAsync(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}