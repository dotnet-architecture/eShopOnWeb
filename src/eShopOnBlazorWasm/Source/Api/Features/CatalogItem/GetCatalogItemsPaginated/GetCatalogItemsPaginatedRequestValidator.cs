namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;
  
  public class GetCatalogItemsPaginatedRequestValidator : AbstractValidator<GetCatalogItemsPaginatedRequest>
  {

    public GetCatalogItemsPaginatedRequestValidator()
    {
      RuleFor(aGetCatalogItemsPaginatedRequest => aGetCatalogItemsPaginatedRequest.PageSize)
        .GreaterThan(0);

      RuleFor(aGetCatalogItemsPaginatedRequest => aGetCatalogItemsPaginatedRequest.PageIndex)
        .GreaterThanOrEqualTo(0);
    }
  }
}