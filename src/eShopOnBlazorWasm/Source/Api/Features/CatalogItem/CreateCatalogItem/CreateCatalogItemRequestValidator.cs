namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using FluentValidation;
  
  public class CreateCatalogItemRequestValidator : AbstractValidator<CreateCatalogItemRequest>
  {

    public CreateCatalogItemRequestValidator()
    {

      RuleFor(aCreateCatalogItemRequest => aCreateCatalogItemRequest.Price)
        .NotEmpty().GreaterThan(0);
      RuleFor(aCreateCatalogItemRequest => aCreateCatalogItemRequest.Name)
        .NotEmpty().MinimumLength(2);
    }
  }
}