namespace GetCatalogItemRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private GetCatalogItemRequestValidator GetCatalogItemRequestValidator { get; set; }

    public void Be_Valid()
    {
      var getCatalogItemRequest = new GetCatalogItemRequest
      {
        // Set Valid values here
        CatalogItemId = 3
      };

      ValidationResult validationResult = GetCatalogItemRequestValidator.TestValidate(getCatalogItemRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_CatalogItemId_is_negative() => GetCatalogItemRequestValidator
      .ShouldHaveValidationErrorFor(aGetCatalogItemRequest => aGetCatalogItemRequest.CatalogItemId, -1);

    public void Setup() => GetCatalogItemRequestValidator = new GetCatalogItemRequestValidator();
  }
}
