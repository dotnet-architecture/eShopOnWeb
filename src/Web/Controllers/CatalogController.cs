using Microsoft.eShopWeb.Services;
using Microsoft.eShopWeb.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Exceptions;

namespace Microsoft.eShopWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ICatalogService _catalogService;
        private readonly IImageService _imageService;

        public CatalogController(IHostingEnvironment env,
            ICatalogService catalogService,
            IImageService imageService)
        {
            _env = env;
            _catalogService = catalogService;
            _imageService = imageService;
        }   


        // GET: /<controller>/
        public async Task<IActionResult> Index(int? BrandFilterApplied, int? TypesFilterApplied, int? page)
        {
            var itemsPage = 10;           
            var catalog = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, BrandFilterApplied, TypesFilterApplied);        

            var vm = new CatalogIndex()
            {
                CatalogItems = catalog.Data,
                Brands = await _catalogService.GetBrands(),
                Types = await _catalogService.GetTypes(),
                BrandFilterApplied = BrandFilterApplied ?? 0,
                TypesFilterApplied = TypesFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = catalog.Data.Count,
                    TotalItems = catalog.Count,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)catalog.Count / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }

        [HttpGet("{id}")]
        [Route("[controller]/pic/{id}")]
        // GET: /<controller>/pic/{id}
        public IActionResult GetImage(int id)
        {
            byte[] imageBytes;
            try
            {
                imageBytes = _imageService.GetImageBytesById(id);
            }
            catch (CatalogImageMissingException ex)
            {
                return NotFound();
            }
            return File(imageBytes, "image/png");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
