namespace eShopOnBlazorWasm.Pipeline.NotificationPreProcessor
{
  using MediatR;
  using MediatR.Pipeline;
  using Microsoft.Extensions.Logging;
  using System.Threading;
  using System.Threading.Tasks;

  internal class PrePipelineNotificationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
  {
    private readonly ILogger Logger;

    private readonly IMediator Mediator;

    public PrePipelineNotificationRequestPreProcessor
            (
      ILogger<PrePipelineNotificationRequestPreProcessor<TRequest>> aLogger,
      IMediator aMediator
    )
    {
      Logger = aLogger;
      Mediator = aMediator;
    }

    public async Task Process(TRequest aRequest, CancellationToken aCancellationToken)
    {
      var notification = new PrePipelineNotification<TRequest>
      {
        Request = aRequest,
      };

      Logger.LogDebug("PrePipelineNotificationRequestPreProcessor");
      await Mediator.Publish(notification, aCancellationToken);
    }
  }
}
