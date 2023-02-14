using System.Text.Json.Serialization;
using Api.Core.Domain.Requests;

namespace Api.Core.Domain.Common;

[JsonDerivedType(typeof(CreateDocumentoResponse), nameof(CreateDocumentoResponse))]
[JsonDerivedType(typeof(CreateClienteResponse), nameof(CreateClienteResponse))]
[JsonDerivedType(typeof(CreateDocumentoDigitalResponse), nameof(CreateDocumentoDigitalResponse))]
public abstract class ApiResponseBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Today;
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
}
