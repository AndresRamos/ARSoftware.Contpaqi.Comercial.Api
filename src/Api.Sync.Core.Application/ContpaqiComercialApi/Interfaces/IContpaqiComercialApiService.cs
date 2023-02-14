using Api.Core.Domain.Common;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;

public interface IContpaqiComercialApiService
{
    Task<IEnumerable<ApiRequestBase>> GetPendingRequestsAsync(CancellationToken cancellationToken);
    Task SendResponseAsync(ApiResponseBase apiResponse, CancellationToken cancellationToken);
}
