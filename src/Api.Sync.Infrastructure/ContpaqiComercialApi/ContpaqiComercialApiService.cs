using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Infrastructure.ContpaqiComercialApi;

public sealed class ContpaqiComercialApiService : IContpaqiComercialApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public ContpaqiComercialApiService(HttpClient httpClient, ILogger<ContpaqiComercialApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<ApiRequest>> GetPendingRequestsAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage message = await _httpClient.GetAsync("api/Requests/Pending", cancellationToken);

        message.EnsureSuccessStatusCode();

        if (message.StatusCode == HttpStatusCode.NoContent)
            return Enumerable.Empty<ApiRequest>();

        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();

        IEnumerable<ApiRequest> apiRequests =
            await message.Content.ReadFromJsonAsync<IEnumerable<ApiRequest>>(options, cancellationToken) ?? Enumerable.Empty<ApiRequest>();

        return apiRequests;
    }

    public async Task SendResponseAsync(Guid apiRequestId, ApiResponse apiResponse, CancellationToken cancellationToken)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();

        string json = JsonSerializer.Serialize(apiResponse, options);
        _logger.LogDebug("JSON Response: {ApiResponse}", json);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponseMessage =
            await _httpClient.PostAsync($"api/requests/{apiRequestId}/response", data, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}
