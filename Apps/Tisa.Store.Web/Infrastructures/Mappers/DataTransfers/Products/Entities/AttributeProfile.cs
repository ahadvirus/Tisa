using AutoMapper;
using Tisa.Store.Web.Models.DataTransfers.Products.Entities;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Products.Entities;

public class AttributeProfile : Profile
{
    public AttributeProfile()
    {
        CreateMap<Attribute, AttributeDTO>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Name))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Type.Name))
            .ForMember(des => des.GetType,
                opt => opt.Ignore());
    }
    
}