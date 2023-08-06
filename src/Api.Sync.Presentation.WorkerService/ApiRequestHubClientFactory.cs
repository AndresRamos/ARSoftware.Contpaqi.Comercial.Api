using Api.Sync.Core.Application.Common.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace Api.Sync.Presentation.WorkerService;

public sealed class ApiRequestHubClientFactory
{
    public static HubConnection BuildConnection(ApiSyncConfig apiSyncConfig)
    {
        HubConnection connection = new HubConnectionBuilder().WithUrl(new Uri($"{apiSyncConfig.BaseAddress}hubs/apiRequestHub"), options =>
            {
                options.Transports = HttpTransportType.WebSockets;
                options.SkipNegotiation = true;
                options.Headers.Add("Ocp-Apim-Subscription-Key", apiSyncConfig.SubscriptionKey);
            })
            .WithAutomaticReconnect()
            .Build();

        return connection;
    }
}
