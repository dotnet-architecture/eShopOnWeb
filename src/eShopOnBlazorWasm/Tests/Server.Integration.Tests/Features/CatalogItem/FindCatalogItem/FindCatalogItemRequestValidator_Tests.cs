namespace FindCatalogItemRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private FindCatalogItemRequestValidator FindCatalogItemRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new FindCatalogItemRequest
      {
        // Set Valid values here
        CatalogBrandId = 3,
        CatalogTypeId = 2
      };

      ValidationResult validationResult = FindCatalogItemRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => FindCatalogItemRequestValidator
      .ShouldHaveValidationErrorFor(aFindCatalogItemRequest => aFindCatalogItemRequest.CatalogBrandId, -1);

    public void Setup() => FindCatalogItemRequestValidator = new FindCatalogItemRequestValidator();
  }
}
