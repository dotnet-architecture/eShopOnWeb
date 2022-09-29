using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Services;

public class CatalogItemViewModelService : ICatalogItemViewModelService
{
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    public CatalogItemViewModelService(IRepository<CatalogItem> catalogItemRepository)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
    {
        var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);
        
        Guard.Against.Null(existingCatalogItem, nameof(existingCatalogItem));
        Guard.Against.NullOrEmpty(viewModel.Name, nameof(viewModel.Name));
        Guard.Against.NullOrEmpty(existingCatalogItem.Description, nameof(existingCatalogItem.Description));
        Guard.Against.NegativeOrZero(viewModel.Price, nameof(viewModel.Price));
        
        existingCatalogItem.UpdateDetails(viewModel.Name, existingCatalogItem.Description, viewModel.Price);
        await _catalogItemRepository.UpdateAsync(existingCatalogItem);
    }
}
