using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface ICatalogLookupDataService<TLookupData> where TLookupData : LookupData
{
    Task<List<TLookupData>> List();
}
