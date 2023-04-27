using AutoMapper;
using Tisa.Store.Web.Models.Entities;
using Tisa.Store.Web.Models.ViewModels.Types;

namespace Tisa.Store.Web.Infrastructures.Mappers.Types;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Type, IndexVM>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Kind
                ));
    }
}