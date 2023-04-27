using System.Linq;
using AutoMapper;
using Tisa.Store.Web.Models.Entities;
using Tisa.Store.Web.Models.ViewModels.Entities;

namespace Tisa.Store.Web.Infrastructures.Mappers.Entities;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Entity, IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(
                des => des.Attributes,
                opt => opt.MapFrom(
                    src => src.Attributes
                )
            )
            ;
    }
    
}