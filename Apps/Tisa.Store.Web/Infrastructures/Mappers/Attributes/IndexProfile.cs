using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.Attributes;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Models.Entities.Attribute, Models.ViewModels.Attributes.IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Title,
                opt => opt.MapFrom(
                    src => src.Name
                ))
            .ForMember(des => des.Display,
                opt => opt.MapFrom(
                    src => src.Title
                ))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Type.Kind
                ))
            .ReverseMap();
    }
}