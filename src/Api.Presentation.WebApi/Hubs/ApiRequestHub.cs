using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace Api.Presentation.WebApi.Hubs;

public class ApiRequestHub : Hub<IApiRequestHubClient>
{
    public static readonly List<Connection> Connections = new();

    public override async Task OnConnectedAsync()
    {
        string groupName = Context.GetHttpContext()!.Request.Headers["Ocp-Apim-Subscription-Key"]!;
        string empresas = Context.GetHttpContext()!.Request.Headers["x-Empresas"]!;
        var empresasList = JsonSerializer.Deserialize<List<string>>(empresas);

        Connections.Add(new Connection { SubscriptionKey = groupName, ConnectionId = Context.ConnectionId, Empresas = empresasList! });

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Connection? connection = Connections.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);

        if (connection is not null) Connections.Remove(connection);

        await base.OnDisconnectedAsync(exception);
    }
}

public class Connection
{
    public string SubscriptionKey { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public List<string> Empresas { get; set; } = new();
}