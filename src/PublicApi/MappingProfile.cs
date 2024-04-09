using System.Linq;
using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>();
        CreateMap<CatalogType, CatalogTypeDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Type));
        CreateMap<CatalogBrand, CatalogBrandDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Brand));
        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.TotalPrice, options => options.MapFrom(src => src.Total()))
            .ForMember(dto => dto.OrderDate, options => options.MapFrom(src => src.OrderDate))
            .ForMember(dto => dto.OrderedProducts, options => options.MapFrom(src =>
                src.OrderItems.Select(item => new ProductDto
                {
                    Id = item.Id,
                    ProductName = item.ItemOrdered.ProductName,
                    Units = item.Units,
                    UnitPrice = item.UnitPrice,
                })));
    }
}
