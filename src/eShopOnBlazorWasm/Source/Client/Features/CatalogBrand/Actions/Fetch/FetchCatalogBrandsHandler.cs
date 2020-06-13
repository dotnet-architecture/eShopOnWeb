namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using BlazorState;
  using MediatR;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;
  using System.Linq;

  internal partial class CatalogBrandState
  {
    public class FetchCatalogBrandsHandler : BaseHandler<FetchCatalogBrandsAction>
    {
      private readonly HttpClient HttpClient;

      public FetchCatalogBrandsHandler(IStore aStore, HttpClient aHttpClient) : base(aStore)
      {
        HttpClient = aHttpClient;
      }

      public override async Task<Unit> Handle
      (
        FetchCatalogBrandsAction aFetchCatalogBrandsAction,
        CancellationToken aCancellationToken
      )
      {
        var getCatalogBrandRequest = new GetCatalogBrandsRequest();
        GetCatalogBrandsResponse getCatalogBrandsResponse =
          await HttpClient.GetFromJsonAsync<GetCatalogBrandsResponse>(getCatalogBrandRequest.RouteFactory);
        CatalogBrandState._CatalogBrands = 
          getCatalogBrandsResponse.CatalogBrands
            .ToDictionary(aCatalogBrand => aCatalogBrand.Id, aCatalogBrand => aCatalogBrand);
        return Unit.Value;
      }
    }
  }
}
