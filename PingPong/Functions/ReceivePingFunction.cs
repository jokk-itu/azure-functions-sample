using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Azure.WebJobs;
using PingPong.Consumers;

namespace PingPong.Functions;
public class ReceivePingFunction
{
  private readonly IMessageReceiver _messageReceiver;

  public ReceivePingFunction(
    IMessageReceiver messageReceiver)
  {
    _messageReceiver = messageReceiver;
  }

  [FunctionName("ReceivePing")]
  public async Task Run(
    [ServiceBusTrigger("ping", Connection = "AzureWebJobsServiceBus")] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
  {
    await _messageReceiver.HandleConsumer<PingConsumer>("ping", message, cancellationToken: cancellationToken);
  }
}