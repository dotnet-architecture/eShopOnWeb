namespace CreateCatalogItemEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using System.Net.Http;
  using System.Net;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Returns : BaseTest
  {
    private readonly CreateCatalogItemRequest CreateCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      CreateCatalogItemRequest = new CreateCatalogItemRequest { };
    }

    public async Task NewCatalogItem()
    {
      var createCatalogItemRequest = new CreateCatalogItemRequest
      {
        
      };

      CreateCatalogItemResponse createCatalogItemResponse =
        await PostJsonAsync<CreateCatalogItemResponse>(CreateCatalogItemRequest.RouteFactory, createCatalogItemRequest);

      ValidateCreateCatalogItemResponse(createCatalogItemResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      CreateCatalogItemRequest.Price = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(CreateCatalogItemRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(CreateCatalogItemRequest.Price));
    }

    private void ValidateCreateCatalogItemResponse(CreateCatalogItemResponse aCreateCatalogItemResponse)
    {
      aCreateCatalogItemResponse.RequestId.Should().Be(CreateCatalogItemRequest.RequestId);
    }

  }
}