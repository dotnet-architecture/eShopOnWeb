namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using BlazorState;
  using MediatR;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  internal partial class CatalogItemState
  {
    public class FetchCatalogItemsHandler : BaseHandler<FetchCatalogItemsAction>
    {
      private readonly HttpClient HttpClient;

      public FetchCatalogItemsHandler(IStore aStore, HttpClient aHttpClient) : base(aStore)
      {
        HttpClient = aHttpClient;
      }

      public override async Task<Unit> Handle
      (
        FetchCatalogItemsAction aFetchCatalogItemsAction,
        CancellationToken aCancellationToken
      )
      {
        var getCatalogItemsPaginatedRequest = 
          new GetCatalogItemsPaginatedRequest 
          { 
            PageSize = CatalogItemState.PageSize,
            PageIndex = CatalogItemState.PageIndex
          };
        GetCatalogItemsPaginatedResponse getCatalogItemsResponse =
          await HttpClient.GetFromJsonAsync<GetCatalogItemsPaginatedResponse>(getCatalogItemsPaginatedRequest.RouteFactory);
        CatalogItemState._CatalogItems = getCatalogItemsResponse.CatalogItems;
        return Unit.Value;
      }
    }
  }
}
