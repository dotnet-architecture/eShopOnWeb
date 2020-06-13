namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;
  
  public class UpdateCatalogItemRequestValidator : AbstractValidator<UpdateCatalogItemRequest>
  {

    public UpdateCatalogItemRequestValidator()
    {

      RuleFor(aCreateCatalogItemRequest => aCreateCatalogItemRequest.Price)
        .NotEmpty().GreaterThan(0);
      RuleFor(aCreateCatalogItemRequest => aCreateCatalogItemRequest.Name)
        .NotEmpty().MinimumLength(2);
    }
  }
}