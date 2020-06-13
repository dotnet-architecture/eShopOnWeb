namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;

  public class DeleteCatalogItemRequestValidator : AbstractValidator<DeleteCatalogItemRequest>
  {

    public DeleteCatalogItemRequestValidator()
    {
      RuleFor(aDeleteCatalogItemRequest => aDeleteCatalogItemRequest.CatalogItemId)
        .NotEmpty().GreaterThan(0);
    }
  }
}