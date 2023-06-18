using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PingPong.Contracts;

namespace PingPong.Functions;
public class SubmitPingFunction
{
  private readonly ILogger<SubmitPingFunction> _logger;
  private readonly IMessageReceiver _messageReceiver;
  private readonly ISendEndpointProvider _sendEndpointProvider;

  public SubmitPingFunction(
    ILogger<SubmitPingFunction> logger,
    IMessageReceiver messageReceiver,
    ISendEndpointProvider sendEndpointProvider
    )
  {
    _logger = logger;
    _messageReceiver = messageReceiver;
    _sendEndpointProvider = sendEndpointProvider;
  }

  [FunctionName("SubmitPing")]
  public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
    CancellationToken cancellationToken)
  {
    var body = await request.ReadAsStringAsync();
    var pingRequest = JsonSerializer.Deserialize<PingRequest>(body);
    _logger.LogInformation("Received Ping Request {@PingRequest}", pingRequest);
    
    var ping = new Ping
    {
      Id = pingRequest.Id
    };
    var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:ping"));
    await endpoint.Send(ping, cancellationToken: cancellationToken);
    _logger.LogInformation("Sending Ping Event {@Ping}", ping);

    return new NoContentResult();
  }
}