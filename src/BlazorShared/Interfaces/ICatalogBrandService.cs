using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<List<CatalogBrand>> List();
        Task<CatalogBrand> GetById(int id);
    }
}
