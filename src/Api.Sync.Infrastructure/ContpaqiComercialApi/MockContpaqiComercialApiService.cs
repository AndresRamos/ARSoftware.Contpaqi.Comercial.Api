using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Infrastructure.ContpaqiComercialApi;

public sealed class MockContpaqiComercialApiService : IContpaqiComercialApiService
{
    private readonly ILogger _logger;

    public MockContpaqiComercialApiService(ILogger<ContpaqiComercialApiService> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<ApiRequestBase>> GetPendingRequestsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting pending requests.");
        await Task.Delay(3000, cancellationToken);
        return Enumerable.Empty<ApiRequestBase>();
    }

    public Task SendResponseAsync(ApiResponseBase apiResponse, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending response.");
        return Task.CompletedTask;
    }
}
