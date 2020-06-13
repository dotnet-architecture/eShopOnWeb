namespace CreateCatalogItemRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private CreateCatalogItemRequestValidator CreateCatalogItemRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new CreateCatalogItemRequest
      {
        // Set Valid values here
        Price = 50.00M
      };

      ValidationResult validationResult = CreateCatalogItemRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => CreateCatalogItemRequestValidator
      .ShouldHaveValidationErrorFor(aCreateCatalogItemRequest => aCreateCatalogItemRequest.Price, -1);

    public void Setup() => CreateCatalogItemRequestValidator = new CreateCatalogItemRequestValidator();
  }
}
