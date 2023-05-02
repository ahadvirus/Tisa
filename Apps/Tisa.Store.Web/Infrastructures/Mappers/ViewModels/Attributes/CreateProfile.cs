using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.ViewModels.Attributes;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<Models.ViewModels.Attributes.CreateVM, Models.Entities.Attribute>()
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Title
                ))
            .ForMember(des => des.Discription,
                opt => opt.MapFrom(
                    src => src.Description
                ))
            .ForMember(des => des.Type,
                opt => opt.Ignore());
    }
}