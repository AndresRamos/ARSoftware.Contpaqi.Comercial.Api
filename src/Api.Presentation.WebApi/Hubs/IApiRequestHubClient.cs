using Api.Core.Domain.Common;

namespace Api.Presentation.WebApi.Hubs;

public interface IApiRequestHubClient
{
    Task GetPendingRequests(GetPendingRequestsMessage getPendingRequestsMessage);
}
