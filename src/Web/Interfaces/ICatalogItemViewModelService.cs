using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface ICatalogItemViewModelService
{
    Task UpdateCatalogItem(CatalogItemViewModel viewModel);
}
