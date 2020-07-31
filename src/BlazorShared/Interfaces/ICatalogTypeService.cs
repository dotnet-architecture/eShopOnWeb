using BlazorShared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorShared.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<List<CatalogType>> List();
        Task<CatalogType> GetById(int id);
    }
}
