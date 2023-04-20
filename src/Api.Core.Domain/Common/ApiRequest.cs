using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Api.Core.Domain.Common;

public sealed class ApiRequest
{
    public ApiRequest(string subscriptionKey, string empresaRfc, IContpaqiRequest contpaqiRequest)
    {
        SubscriptionKey = subscriptionKey;
        EmpresaRfc = empresaRfc;
        ContpaqiRequestType = contpaqiRequest.GetType().Name;
        ContpaqiRequest = contpaqiRequest;
    }

    /// <summary>
    ///     Id de la solicitud.
    /// </summary>
    [JsonInclude]
    public Guid Id { get; private set; }

    /// <summary>
    ///     Licencia del consumidor de la API.
    /// </summary>
    [JsonInclude]
    public string SubscriptionKey { get; private set; }

    /// <summary>
    ///     RFC de la empresa.
    /// </summary>
    [JsonInclude]
    public string EmpresaRfc { get; private set; }

    /// <summary>
    ///     Fecha de creacion de la solicitud.
    /// </summary>
    [JsonInclude]
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;

    /// <summary>
    ///     Tipo de solicitud CONTPAQi
    /// </summary>
    [JsonInclude]
    public string ContpaqiRequestType { get; private set; }

    /// <summary>
    ///     Solicitud CONTPAQi.
    /// </summary>
    [JsonInclude]
    public IContpaqiRequest ContpaqiRequest { get; private set; }

    /// <summary>
    ///     Estatus de la solicitud.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonInclude]
    public RequestStatus Status { get; private set; } = RequestStatus.Pending;

    /// <summary>
    ///     Respuesta de la solicitud.
    /// </summary>
    [JsonInclude]
    public ApiResponse? Response { get; private set; }

    public void SetResponse(ApiResponse response)
    {
        Response = response;
        Status = RequestStatus.Processed;
    }
}
