namespace DeleteCatalogItemRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private DeleteCatalogItemRequestValidator DeleteCatalogItemRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new DeleteCatalogItemRequest
      {
        // Set Valid values here
        CatalogItemId = 5
      };

      ValidationResult validationResult = DeleteCatalogItemRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => DeleteCatalogItemRequestValidator
      .ShouldHaveValidationErrorFor(aDeleteCatalogItemRequest => aDeleteCatalogItemRequest.CatalogItemId, -1);

    public void Setup() => DeleteCatalogItemRequestValidator = new DeleteCatalogItemRequestValidator();
  }
}
