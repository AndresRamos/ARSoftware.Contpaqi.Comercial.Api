using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sync.Core.Application.Api.Interfaces;

public interface IContpaqiComercialApiService
{
    Task<IEnumerable<ApiRequest>> GetPendingRequestsAsync(CancellationToken cancellationToken);
    Task SendResponseAsync(Guid apiRequestId, ApiResponse apiResponse, CancellationToken cancellationToken);
}
