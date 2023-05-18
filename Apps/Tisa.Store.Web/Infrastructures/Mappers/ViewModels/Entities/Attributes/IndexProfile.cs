using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Entities.Attributes;

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
            .ForMember(des => des.Description,
                opt => opt.MapFrom(
                    src => src.Attribute.Description
                ))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Attribute.Type.Name
                ));
    }
    
}