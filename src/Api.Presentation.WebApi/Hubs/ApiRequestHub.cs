using Microsoft.AspNetCore.SignalR;

namespace Api.Presentation.WebApi.Hubs;

public class ApiRequestHub : Hub<IApiRequestHubClient>
{
    public override async Task OnConnectedAsync()
    {
        string groupName = Context.GetHttpContext()!.Request.Headers["Ocp-Apim-Subscription-Key"]!;

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await base.OnConnectedAsync();
    }
}
