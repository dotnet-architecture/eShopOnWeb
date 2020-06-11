namespace GetCatalogTypesRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.Catalogs;

  public class Validate_Should
  {
    private GetCatalogTypesRequestValidator GetCatalogTypesRequestValidator { get; set; }

    public void Be_Valid()
    {
      var __requestName__Request = new GetCatalogTypesRequest
      {
        // Set Valid values here
        Days = 5
      };

      ValidationResult validationResult = GetCatalogTypesRequestValidator.TestValidate(__requestName__Request);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => GetCatalogTypesRequestValidator
      .ShouldHaveValidationErrorFor(aGetCatalogTypesRequest => aGetCatalogTypesRequest.Days, -1);

    public void Setup() => GetCatalogTypesRequestValidator = new GetCatalogTypesRequestValidator();
  }
}
