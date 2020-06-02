namespace eShopOnBlazorWasm.Components
{
  using Microsoft.AspNetCore.Components;
  using eShopOnBlazorWasm.Features.Bases;

  public partial class SurveyPrompt: BaseComponent
  {
    [Parameter]
    public string Title { get; set; } // Demonstrates how a parent component can supply parameters
  }
}
