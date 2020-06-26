using AutoMapper;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Web.API.CatalogItemEndpoints;

namespace Microsoft.eShopWeb.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogItem, CatalogItemDto>();
        }
    }
}
