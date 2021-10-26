using BlazorShared.Models;
using System.Collections.Generic;

namespace BlazorShared.Interfaces
{
    public interface ILookupDataResponse<TLookupData> where TLookupData : LookupData
    {
        List<TLookupData> List { get; set; }
    }
}
