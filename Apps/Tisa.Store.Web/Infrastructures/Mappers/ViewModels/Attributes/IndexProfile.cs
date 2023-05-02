using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Attributes;

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
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Discription
                ))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Type.Name
                ))
            .ReverseMap();
    }
}