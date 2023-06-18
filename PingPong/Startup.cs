using MassTransit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PingPong;
using PingPong.Consumers;
using PingPong.Functions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace PingPong;
public class Startup : FunctionsStartup
{
  public override void Configure(IFunctionsHostBuilder builder)
  {
    builder.Services
      .AddScoped<ReceivePingFunction>()
      .AddScoped<ReceivePongFunction>()
      .AddScoped<SubmitPingFunction>()
      .AddMassTransitForAzureFunctions(cfg =>
      {
        cfg.AddConsumersFromNamespaceContaining<ConsumerNamespace>();
      }, "AzureWebJobsServiceBus");
  }
}