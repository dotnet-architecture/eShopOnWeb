using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

/// <summary>
/// Specific query used to fetch count without running in memory
/// </summary>
public interface IBasketQueryService
{
    Task<int> CountTotalBasketItemsAsync(string username);
}

