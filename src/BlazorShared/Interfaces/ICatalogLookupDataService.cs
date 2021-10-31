using BlazorShared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorShared.Interfaces
{
    public interface ICatalogLookupDataService<TLookupData> where TLookupData : LookupData
    {
        Task<List<TLookupData>> List();
    }
}
