namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;
  
  public class FindCatalogItemRequestValidator : AbstractValidator<FindCatalogItemRequest>
  {

    public FindCatalogItemRequestValidator()
    {
      RuleFor(aFindCatalogItemRequest => aFindCatalogItemRequest.CatalogBrandId)
        .GreaterThan(0);

      RuleFor(aFindCatalogItemRequest => aFindCatalogItemRequest.CatalogBrandId)
        .GreaterThan(0);
    }
  }
}