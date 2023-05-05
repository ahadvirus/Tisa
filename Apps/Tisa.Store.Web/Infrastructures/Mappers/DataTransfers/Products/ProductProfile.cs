using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Tisa.Store.Web.Infrastructures.Mappers.DataTransfers.Products;

public class ProductProfile : Profile
{

    public ProductProfile()
    {

        CreateMap<List<Models.Entities.Product>, Models.DataTransfers.Products.ProductDTO>()
            .ForMember(des => des.Properties,
                opt => opt.MapFrom(
                    src => src.Select(product => product.AttributeEntity)
                ));

    }
}