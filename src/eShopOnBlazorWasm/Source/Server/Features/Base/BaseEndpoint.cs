namespace eShopOnBlazorWasm.Features.Bases
{
  using MediatR;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.DependencyInjection;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  [ApiController]
  [Produces("application/json")]
  public class BaseEndpoint<TRequest, TResponse> : ControllerBase
      where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse
  {
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected virtual async Task<IActionResult> Send(TRequest aRequest)
    {
      TResponse response = await Mediator.Send(aRequest);

      return Ok(response);
    }
  }
}
