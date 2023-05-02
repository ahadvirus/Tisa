using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Validators;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Models.Entities.Validator, Models.ViewModels.Validators.IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                    ))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Name
                ))
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Description
                ));
    }
}