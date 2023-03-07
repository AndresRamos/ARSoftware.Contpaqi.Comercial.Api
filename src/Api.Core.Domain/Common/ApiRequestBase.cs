using System.Text.Json.Serialization;
using MediatR;

namespace Api.Core.Domain.Common;

public abstract class ApiRequestBase : IRequest<ApiResponseBase>
{
    public Guid Id { get; set; }

    public string SubscriptionKey { get; set; } = Guid.Empty.ToString("N");

    public string EmpresaRfc { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; } = DateTime.Now;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

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
