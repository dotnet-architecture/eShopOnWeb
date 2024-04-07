using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

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
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
            .ForMember(dto => dto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dto => dto.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus)).ReverseMap();

        CreateMap<Order, OrderDetailDto>()
            .ForMember(dto => dto.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dto => dto.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus));

        CreateMap<OrderItem, OrderDetailDto>()
            .ForMember(dto => dto.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dto => dto.Units, opt => opt.MapFrom(src => src.Units))
            .ForMember(dto => dto.ProductName, opt => opt.MapFrom(src => src.ItemOrdered.ProductName)).ReverseMap();
    }
}
