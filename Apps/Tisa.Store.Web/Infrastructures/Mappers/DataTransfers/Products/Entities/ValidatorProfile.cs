using System.Linq;
using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Products.Entities;

public class ValidatorProfile : Profile
{
    public ValidatorProfile()
    {
        CreateMap<Models.Entities.AttributeEntityValidator, Models.DataTransfers.Products.Entities.AttributeValidatorDTO>()
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Validator.Name
                ))
            .ForMember(des => des.Parameters,
                opt => opt.MapFrom(
                    src => src.Claims
                        .Where(claim =>
                            claim.Key == nameof(Contracts.Claims.AttributeEntityValidator.IParameter.Parameters))
                        .Select(claim => claim.Value)
                        .First()
                ))
            .ForMember(des => des.ParametersValidator,
                opt => opt.Ignore())
            .ForMember(des => des.Validator,
                opt => opt.MapFrom(
                    src => src.Validator.Claims
                        .Where(claim =>
                            claim.Key == nameof(Contracts.Claims.Validators.IParameter.ParametersValidator))
                        .Select(claim => claim.Value)
                        .First()
                ))
            .ForMember(des => des.ValidatorBuilder,
                opt => opt.Ignore())
            .ForMember(des => des.Builder,
                opt => opt.MapFrom(
                    src => src.Validator.Claims
                        .Where(claim =>
                            claim.Key == nameof(Contracts.Claims.Validators.IBuilder.ValidatorBuilder))
                        .Select(claim => claim.Value)
                        .First()
                ));
        
        CreateMap<Models.Entities.AttributeEntityValidator, Infrastructures.Contracts.DataTransfers.IValidatorDTO>()
            .As<Models.DataTransfers.Products.Entities.AttributeValidatorDTO>();

    }
}