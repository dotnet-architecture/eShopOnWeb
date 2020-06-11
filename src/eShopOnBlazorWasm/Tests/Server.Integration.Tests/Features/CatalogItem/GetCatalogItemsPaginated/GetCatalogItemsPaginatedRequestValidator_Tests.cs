namespace GetCatalogItemsPaginatedRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Validate_Should
  {
    private GetCatalogItemsPaginatedRequestValidator GetCatalogItemsPaginatedRequestValidator { get; set; }

    public void Be_Valid()
    {
      var getCatalogItemsPaginatedRequest = new GetCatalogItemsPaginatedRequest
      {
        // Set Valid values here
        PageIndex = 2,
        PageSize = 3
      };

      ValidationResult validationResult = GetCatalogItemsPaginatedRequestValidator.TestValidate(getCatalogItemsPaginatedRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_Days_are_negative() => GetCatalogItemsPaginatedRequestValidator
      .ShouldHaveValidationErrorFor(aGetCatalogItemsPaginatedRequest => aGetCatalogItemsPaginatedRequest.PageIndex, -1);

    public void Setup() => GetCatalogItemsPaginatedRequestValidator = new GetCatalogItemsPaginatedRequestValidator();
  }
}
