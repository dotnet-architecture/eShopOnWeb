namespace eShopOnBlazorWasm.Features.CatalogItems.Components
{
  using eShopOnBlazorWasm.Features.Bases;
  using eShopOnBlazorWasm.Features.CatalogBrands;
  using eShopOnBlazorWasm.Features.CatalogTypes;
  using Microsoft.AspNetCore.Components;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using static BlazorState.Features.Routing.RouteState;

  public partial class Create: BaseComponent
  {
    public CreateCatalogItemRequest CreateCatalogItemRequest { get; set; }

    private IReadOnlyList<CatalogBrandDto> CatalogBrands => CatalogBrandState.CatalogBrandsAsList;
    private IReadOnlyList<CatalogTypeDto> CatalogTypes => CatalogTypeState.CatalogTypesAsList;

    [Parameter] public string RedirectRoute { get; set; }

    protected override Task OnInitializedAsync()
    {
      CreateCatalogItemRequest = new CreateCatalogItemRequest();
      
      return base.OnInitializedAsync();
    }



    protected async Task HandleValidSubmit()
    {
      _ = await Mediator.Send(new CreateCatalogItemAction { CreateCatalogItemRequest = CreateCatalogItemRequest });
      _ = await Mediator.Send(new ChangeRouteAction { NewRoute = RedirectRoute });
    }
  }
}
