namespace eShopOnBlazorWasm.Pipeline.NotificationPostProcessor
{
  using MediatR;
  using MediatR.Pipeline;
  using Microsoft.Extensions.Logging;
  using System.Threading;
  using System.Threading.Tasks;

  internal class PostPipelineNotificationRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
  {
    private readonly ILogger Logger;

    private readonly IMediator Mediator;

    public PostPipelineNotificationRequestPostProcessor
            (
      ILogger<PostPipelineNotificationRequestPostProcessor<TRequest, TResponse>> aLogger,
      IMediator aMediator
    )
    {
      Logger = aLogger;
      Mediator = aMediator;
    }

    public async Task Process(TRequest aRequest, TResponse aResponse, CancellationToken aCancellationToken)
    {
      var notification = new PostPipelineNotification<TRequest, TResponse>
      {
        Request = aRequest,
        Response = aResponse
      };

      Logger.LogDebug("PostPipelineNotificationRequestPostProcessor");
      await Mediator.Publish(notification, aCancellationToken);
    }
  }
}
