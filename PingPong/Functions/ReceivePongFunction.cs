using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Azure.WebJobs;
using PingPong.Consumers;

namespace PingPong.Functions;
public class ReceivePongFunction
{
  private readonly IMessageReceiver _messageReceiver;

  public ReceivePongFunction(
    IMessageReceiver messageReceiver)
  {
    _messageReceiver = messageReceiver;
  }

  [FunctionName("ReceivePong")]
  public async Task Run(
    [ServiceBusTrigger("pong", Connection = "AzureWebJobsServiceBus")] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
  {
    await _messageReceiver.HandleConsumer<PongConsumer>("pong", message, cancellationToken: cancellationToken);
  }
}
