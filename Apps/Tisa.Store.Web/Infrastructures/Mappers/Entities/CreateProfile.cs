using AutoMapper;
using Tisa.Store.Web.Models.Entities;
using Tisa.Store.Web.Models.ViewModels.Entities;

namespace Tisa.Store.Web.Infrastructures.Mappers.Entities;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateVM, Entity>()
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Name
                    ))
            .ForMember(des => des.Attributes,
            opt => opt.Ignore());
    }
}