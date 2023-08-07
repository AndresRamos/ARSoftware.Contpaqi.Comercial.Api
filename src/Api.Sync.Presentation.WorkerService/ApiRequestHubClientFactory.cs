using Api.Sync.Core.Application.Common.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class ApiRequestHubClientFactory
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly IHostEnvironment _environment;

    public ApiRequestHubClientFactory(IOptions<ApiSyncConfig> apiSyncConfigOptionss, IHostEnvironment environment)
    {
        _apiSyncConfig = apiSyncConfigOptionss.Value;
        _environment = environment;
    }

    public HubConnection BuildConnection()
    {
        Uri webSocketUrl = _environment.IsDevelopment()
            ? new Uri($"{_apiSyncConfig.BaseAddress}hubs/apiRequestHub")
            : new Uri("https://contpaqicomercialapi.azurewebsites.net/hubs/apiRequestHub");

        HubConnection connection = new HubConnectionBuilder().WithUrl(webSocketUrl, options =>
            {
                options.Transports = HttpTransportType.WebSockets;
                options.SkipNegotiation = true;
                options.Headers.Add("Ocp-Apim-Subscription-Key", _apiSyncConfig.SubscriptionKey);
            })
            .WithAutomaticReconnect()
            .Build();

        return connection;
    }
}
