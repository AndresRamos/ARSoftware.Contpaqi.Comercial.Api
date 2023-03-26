using System.Text.Json.Serialization;
using MediatR;

namespace Api.Core.Domain.Common;

public abstract class ApiRequestBase : IRequest<ApiResponseBase>
{
    /// <summary>
    ///     Id de la solicitud.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Licencia del consumidor de la API.
    /// </summary>
    public string SubscriptionKey { get; set; } = Guid.Empty.ToString("N");

    /// <summary>
    ///     RFC de la empresa.
    /// </summary>
    public string EmpresaRfc { get; set; } = string.Empty;

    /// <summary>
    ///     Fecha de creacion de la solicitud.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.Now;

    /// <summary>
    ///     Estatus de la solicitud.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    /// <summary>
    ///     Respuesta de la solicitud.
    /// </summary>
    public ApiResponseBase? Response { get; set; }

    public void SetCreateDefaults(string subscriptionKey)
    {
        Id = Guid.Empty;
        SubscriptionKey = subscriptionKey;
        DateCreated = DateTime.Now;
        Status = RequestStatus.Pending;
        Response = null;
    }
}
