using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.Entities.Attributes;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Models.Entities.AttributeEntity, Models.ViewModels.Attributes.IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Title,
                opt => opt.MapFrom(
                    src => src.Attribute.Name
                ))
            .ForMember(des => des.Display,
                opt => opt.MapFrom(
                    src => src.Attribute.Title
                ))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Attribute.Type.Kind
                ));
    }
    
}