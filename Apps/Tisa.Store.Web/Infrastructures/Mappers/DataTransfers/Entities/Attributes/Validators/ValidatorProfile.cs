using System.Linq;
using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Entities.Attributes.Validators;

public class ValidatorProfile : Profile
{
    public ValidatorProfile()
    {
        CreateMap<Models.Entities.Validator, Models.DataTransfers.Entities.Attributes.Validators.ValidatorDTO>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Description))
            .ForMember(des => des.Types,
                opt => opt.MapFrom(
                    src => src.Types.Select(type => type.TypeId)))
            .ForMember(des => des.Validator,
                opt => opt.MapFrom(
                    src => src.Claims
                        .Where(claim =>
                            claim.Key == nameof(Models.DataTransfers.Entities.Attributes.Validators.ValidatorDTO
                                .ParametersValidator))
                        .Select(claim => claim.Value)
                        .FirstOrDefault()))
            .ForMember(des => des.ParametersValidator,
                opt => opt.Ignore());
    }
    
}