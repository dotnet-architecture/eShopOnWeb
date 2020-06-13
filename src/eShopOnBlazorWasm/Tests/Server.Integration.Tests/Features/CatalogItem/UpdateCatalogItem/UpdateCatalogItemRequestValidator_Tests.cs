namespace UpdateCatalogItemRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private UpdateCatalogItemRequestValidator UpdateCatalogItemRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new UpdateCatalogItemRequest
      {
        // Set Valid values here
        CatalogItemId = 5
      };

      ValidationResult validationResult = UpdateCatalogItemRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => UpdateCatalogItemRequestValidator
      .ShouldHaveValidationErrorFor(aUpdateCatalogItemRequest => aUpdateCatalogItemRequest.CatalogItemId, -1);

    public void Setup() => UpdateCatalogItemRequestValidator = new UpdateCatalogItemRequestValidator();
  }
}
