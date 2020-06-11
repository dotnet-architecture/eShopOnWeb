namespace eShopOnBlazorWasm.Features.Catalogs
{
  using MediatR;
  using System.Text.Json.Serialization;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogTypesRequest : BaseApiRequest, IRequest<GetCatalogTypesResponse>
  {
    public const string Route = "api/Catalog/GetCatalogTypes";

    /// <summary>
    /// The Number of days of forecasts to get
    /// </summary>
    /// <example>5</example>
    public int Days { get; set; }

    internal override string RouteFactory => $"{Route}?{nameof(Days)}={Days}&{nameof(Id)}={Id}";
  }
}