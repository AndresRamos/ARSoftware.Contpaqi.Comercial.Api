using System.Text;
using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;
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

    public async Task<IEnumerable<ApiRequestBase>> GetPendingRequestsAsync(CancellationToken cancellationToken)
    {
        Stream stream = await _httpClient.GetStreamAsync("api/Requests/Pending", cancellationToken);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        return await JsonSerializer.DeserializeAsync<IEnumerable<ApiRequestBase>>(stream, options, cancellationToken) ??
               Enumerable.Empty<ApiRequestBase>();
    }

    public async Task SendResponseAsync(ApiResponseBase apiResponse, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        string json = JsonSerializer.Serialize(apiResponse, options);
        _logger.LogDebug("JSON Response: {ApiResponse}", json);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("api/Responses", data, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}
