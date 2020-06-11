namespace GetCatalogBrandsRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.Catalogs;

  public class Validate_Should
  {
    private GetCatalogBrandsRequestValidator GetCatalogBrandsRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new GetCatalogBrandsRequest
      {
        // Set Valid values here
        Days = 5
      };

      ValidationResult validationResult = GetCatalogBrandsRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => GetCatalogBrandsRequestValidator
      .ShouldHaveValidationErrorFor(aGetCatalogBrandsRequest => aGetCatalogBrandsRequest.Days, -1);

    public void Setup() => GetCatalogBrandsRequestValidator = new GetCatalogBrandsRequestValidator();
  }
}
