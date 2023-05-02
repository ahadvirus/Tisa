using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Entities.Attributes.Validators;

public class AttributeProfile : Profile
{
    public AttributeProfile()
    {
        CreateMap<Models.Entities.Attribute, Models.DataTransfers.Entities.Attributes.Validators.AttributeDTO>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Name))
            .ForMember(des => des.TypeId,
                opt => opt.MapFrom(
                    src => src.TypeId))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Type.Name))
            ;
    }
}