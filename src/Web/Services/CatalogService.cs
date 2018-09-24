using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Microsoft.eShopWeb.Comm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.eShopWeb.Web.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
    /// with UI-specific types (view models and SelectListItem types).
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<ApplicationCore.Entities.CatalogItem> _itemRepository;
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;
        private readonly IAsyncRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogService> _logger;
        private readonly string _remoteServiceBaseUrl;

        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _remoteServiceBaseUrl = $"{httpClient.BaseAddress}/api/v1/catalog/";
        }

        // public CatalogService(
        //     ILoggerFactory loggerFactory,
        //     IRepository<CatalogItem> itemRepository,
        //     IAsyncRepository<CatalogBrand> brandRepository,
        //     IAsyncRepository<CatalogType> typeRepository,
        //     IUriComposer uriComposer)
        // {
        //     _logger = loggerFactory.CreateLogger<CatalogService>();
        //     _itemRepository = itemRepository;
        //     _brandRepository = brandRepository;
        //     _typeRepository = typeRepository;
        //     _uriComposer = uriComposer;
        // }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            _logger.LogInformation($"Catalog service URL is : {_remoteServiceBaseUrl}");
            var uri = API.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl, pageIndex, itemsPage, brandId, typeId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var catalog = JsonConvert.DeserializeObject<Catalog>(responseString);

            // var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            // var root = _itemRepository.List(filterSpecification);

            // var totalItems = root.Count();

            // var itemsOnPage = root
            //     .Skip(itemsPage * pageIndex)
            //     .Take(itemsPage)
            //     .ToList();

            // itemsOnPage.ForEach(x =>
            // {
            //     x.PictureUri = _uriComposer.ComposePicUri(x.PictureUri);
            // });

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalog.Data.Select(i => new CatalogItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = i.PictureUri,
                    Price = i.Price
                }),
                Brands = await GetBrands(),
                Types = await GetTypes(),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = catalog.Data.Count,
                    TotalItems = catalog.Count,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)catalog.Count / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }
 
        // public async Task<IEnumerable<SelectListItem>> GetBrands()
        // {
        //     _logger.LogInformation("GetBrands called.");
        //     var brands = await _brandRepository.ListAllAsync();

        //     var items = new List<SelectListItem>
        //     {
        //         new SelectListItem() { Value = null, Text = "All", Selected = true }
        //     };
        //     foreach (CatalogBrand brand in brands)
        //     {
        //         items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand });
        //     }

        //     return items;
        // }

        // public async Task<IEnumerable<SelectListItem>> GetTypes()
        // {
        //     _logger.LogInformation("GetTypes called.");
        //     var types = await _typeRepository.ListAllAsync();
        //     var items = new List<SelectListItem>
        //     {
        //         new SelectListItem() { Value = null, Text = "All", Selected = true }
        //     };
        //     foreach (CatalogType type in types)
        //     {
        //         items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });
        //     }

        //     return items;
        // }
        
        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            var uri = API.Catalog.GetAllBrands(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = null, Text = "All", Selected = true });

            var brands = JArray.Parse(responseString);

            foreach (var brand in brands.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = brand.Value<string>("id"),
                    Text = brand.Value<string>("brand")
                });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            var uri = API.Catalog.GetAllTypes(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = null, Text = "All", Selected = true });

            var brands = JArray.Parse(responseString);
            foreach (var brand in brands.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = brand.Value<string>("id"),
                    Text = brand.Value<string>("type")
                });
            }

            return items;
        }
    }
}
