using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PingPong.Contracts;

namespace PingPong.Consumers;
public class PingConsumer : IConsumer<Ping>
{
  private readonly ILogger<PingConsumer> _logger;

  public PingConsumer(
    ILogger<PingConsumer> logger)
  {
    _logger = logger;
   
  }

  public async Task Consume(ConsumeContext<Ping> context)
  {
    _logger.LogInformation("Received Ping Event {@Ping}", context.Message);
    var pong = new Pong
    {
      Id = context.Message.Id
    };
    var endpoint = await context.GetSendEndpoint(new Uri("queue:pong"));
    await endpoint.Send(pong, context.CancellationToken);
    _logger.LogInformation("Sending Pong Event {@Pong}", pong);
  }
}