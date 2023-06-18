using MassTransit;
using System;

namespace PingPong.Consumers;
public class PingConsumerDefinition : ConsumerDefinition<PingConsumer>
{
  protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PingConsumer> consumerConfigurator)
  {
    base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    endpointConfigurator.UseMessageRetry(retryConfigurator =>
    {
      retryConfigurator.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromHours(2), TimeSpan.FromSeconds(15));
    });
  }
}