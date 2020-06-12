namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;
  
  public class GetCatalogItemRequestValidator : AbstractValidator<GetCatalogItemRequest>
  {

    public GetCatalogItemRequestValidator()
    {
      RuleFor(aGetCatalogItemRequest => aGetCatalogItemRequest.CatalogItemId)
        .NotEmpty().GreaterThan(0);
    }
  }
}