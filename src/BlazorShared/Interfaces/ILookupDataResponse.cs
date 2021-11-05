using System.Collections.Generic;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface ILookupDataResponse<TLookupData> where TLookupData : LookupData
{
    List<TLookupData> List { get; set; }
}
