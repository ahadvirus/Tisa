using System.Linq;
using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Products.Entities;

public class AttributeValidatorProfile : Profile
{
    public AttributeValidatorProfile()
    {
        CreateMap<Models.Entities.AttributeEntity, Models.DataTransfers.Products.Entities.AttributeEntityDTO>()
            .ForMember(des => des.Id,
                opt => opt.MapFrom(
                    src => src.Id
                ))
            .ForMember(des => des.Name,
                opt => opt.MapFrom(
                    src => src.Attribute.Name
                ))
            .ForMember(des => des.Type,
                opt => opt.MapFrom(
                    src => src.Attribute.Type.Name
                ))
            .ForMember(des => des.GetType,
                opt => opt.Ignore())
            .ForMember(des => des.Validators,
                opt => opt.MapFrom(
                    src => src.Validators
                        .Where(validator =>
                            validator.Validator.Types.Any(type => type.TypeId == src.Attribute.TypeId))
                ));

        CreateMap<Models.Entities.AttributeEntity, Infrastructures.Contracts.DataTransfers.IAttributeEntityDTO>()
            .As<Models.DataTransfers.Products.Entities.AttributeEntityDTO>();

        CreateMap<Models.DataTransfers.Products.Entities.AttributeEntityDTO,
            Models.DataTransfers.Products.Entities.AttributeDTO>();

        CreateMap<
            Infrastructures.Contracts.DataTransfers.IAttributeEntityDTO,
            Models.DataTransfers.Products.Entities.AttributeDTO>();
            
        CreateMap<Infrastructures.Contracts.DataTransfers.IAttributeEntityDTO,
                Infrastructures.Contracts.DataTransfers.IAttributeDTO>()
            .As<Models.DataTransfers.Products.Entities.AttributeDTO>();
    }
}