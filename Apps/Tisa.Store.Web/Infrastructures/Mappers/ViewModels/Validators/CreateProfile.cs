using System.Collections.Generic;
using AutoMapper;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Validators;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<Models.ViewModels.Validators.CreateVM, Validator>()
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Name
                ))
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Description
                ))
            .ForMember(des => des.Claims,
                opt => opt.MapFrom(
                    src => new List<ValidatorClaim>()
                    {
                        new ValidatorClaim()
                        {
                            Key = nameof(src.ValidatorBuilder),
                            Value = src.ValidatorBuilder.GetType().FullName ??
                                    string.Empty
                        },
                        new ValidatorClaim()
                        {
                            Key = nameof(src.ParametersValidator),
                            Value = src.ParametersValidator.GetType().FullName ??
                                    string.Empty
                        }
                    }
                ))
            .ForMember(des => des.Types,
                opt => opt.Ignore());;
    }
}