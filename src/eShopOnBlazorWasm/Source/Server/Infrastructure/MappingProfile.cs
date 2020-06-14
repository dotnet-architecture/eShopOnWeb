namespace eShopOnBlazorWasm.Infrastructure
{
  using AutoMapper;
  using eShopOnBlazorWasm.Features.CatalogBrands;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using eShopOnBlazorWasm.Features.CatalogTypes;
  using Microsoft.eShopWeb.ApplicationCore.Entities;

  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CatalogType, CatalogTypeDto>();
      CreateMap<CatalogBrand, CatalogBrandDto>();
      CreateMap<CatalogItem, CatalogItemDto>();
      CreateMap<CreateCatalogItemRequest, CatalogItem>(MemberList.Source);
      CreateMap<UpdateCatalogItemRequest, CatalogItem>(MemberList.Source);
    }
  }
}
