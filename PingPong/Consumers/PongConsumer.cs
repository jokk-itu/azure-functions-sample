using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PingPong.Contracts;

namespace PingPong.Consumers;
public class PongConsumer : IConsumer<Pong>
{
  private readonly ILogger<PongConsumer> _logger;

  public PongConsumer(
    ILogger<PongConsumer> logger)
  {
    _logger = logger;
  }

  public Task Consume(ConsumeContext<Pong> context)
  {
    _logger.LogInformation("Received Pong Event {@Pong}", context.Message);
    return Task.CompletedTask;
  }
}
