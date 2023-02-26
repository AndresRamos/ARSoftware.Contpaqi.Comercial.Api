using System.Text.Json.Serialization;
using MediatR;

namespace Api.Core.Domain.Common;

//[JsonDerivedType(typeof(CrearClienteRequest), nameof(CrearClienteRequest))]
public abstract class ApiRequestBase : IRequest<ApiResponseBase>
{
    public Guid Id { get; set; }
    public string EmpresaRfc { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    public ApiResponseBase? Response { get; set; }

    public void SetCreateDefaults()
    {
        Id = Guid.Empty;
        DateCreated = DateTime.Now;
        Status = RequestStatus.Pending;
        Response = null;
    }
}
