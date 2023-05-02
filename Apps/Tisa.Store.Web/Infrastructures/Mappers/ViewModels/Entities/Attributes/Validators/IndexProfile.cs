using System.Linq;
using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Entities.Attributes.Validators;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Models.Entities.AttributeEntityValidator,
                Models.ViewModels.Entities.Attributes.Validators.IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Validator.Name
                ))
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Validator.Description
                ))
            .ForMember(des => des.Parameters,
                opt => opt.MapFrom(
                    src => src.Claims
                        .Where(claim =>
                            claim.Key == nameof(Models.ViewModels.Entities.Attributes.Validators.IndexVM
                                .Parameters))
                        .Select(claim => claim.Value)
                        .FirstOrDefault()
                ));
    }
}