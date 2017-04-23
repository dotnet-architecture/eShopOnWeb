using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.eShopWeb.Infrastructure;
using Microsoft.eShopWeb.ViewModels;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogContext _context;
        private readonly IOptionsSnapshot<CatalogSettings> _settings;
        private readonly ILogger<CatalogService> _logger;
        
        public CatalogService(CatalogContext context, 
            IOptionsSnapshot<CatalogSettings> settings,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _settings = settings;
            _logger = loggerFactory.CreateLogger<CatalogService>();
        }

        public async Task<Catalog> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            _logger.LogInformation("GetCatalogItems called.");
            var root = (IQueryable<CatalogItem>)_context.CatalogItems;

            if (typeId.HasValue)
            {
                root = root.Where(ci => ci.CatalogTypeId == typeId);
            }

            if (brandId.HasValue)
            {
                root = root.Where(ci => ci.CatalogBrandId == brandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToListAsync();

            itemsOnPage = ComposePicUri(itemsOnPage);

            return new Catalog() { Data = itemsOnPage, PageIndex = pageIndex, Count = (int)totalItems };           
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("GetBrands called.");
            var brands = await _context.CatalogBrands.ToListAsync();

//// create
//var newBrand = new CatalogBrand() { Brand = "Acme" };
//_context.Add(newBrand);
//await _context.SaveChangesAsync();

//// read and update
//var existingBrand = _context.Find<CatalogBrand>(1);
//existingBrand.Brand = "Updated Brand";
//await _context.SaveChangesAsync();

//// delete
//var brandToDelete = _context.Find<CatalogBrand>(2);
//_context.CatalogBrands.Remove(brandToDelete);
//await _context.SaveChangesAsync();

//var brandsWithItems = await _context.CatalogBrands
//    .Include(b => b.Items)
//    .ToListAsync();


            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogBrand brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("GetTypes called.");
            var types = await _context.CatalogTypes.ToListAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogType type in types)
            {
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });
            }

            return items;
        }

        private List<CatalogItem> ComposePicUri(List<CatalogItem> items)
        {
            var baseUri = _settings.Value.CatalogBaseUrl;                      
            items.ForEach(x =>
            {
                x.PictureUri = x.PictureUri.Replace("http://catalogbaseurltobereplaced", baseUri);
            });

            return items;
        }

        //public async Task<IEnumerable<CatalogType>> GetCatalogTypes()
        //{
        //    return await _context.CatalogTypes.ToListAsync();
        //}

        //private readonly SqlConnection _conn;
        //public async Task<IEnumerable<CatalogType>> GetCatalogTypesWithDapper()
        //{
        //    return await _conn.QueryAsync<CatalogType>("SELECT * FROM CatalogType");
        //}
    }
}
