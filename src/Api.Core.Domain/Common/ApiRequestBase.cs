using System.Text.Json.Serialization;
using Api.Core.Domain.Requests;

namespace Api.Core.Domain.Common;

[JsonDerivedType(typeof(CreateDocumentoRequest), nameof(CreateDocumentoRequest))]
[JsonDerivedType(typeof(CreateClienteRequest), nameof(CreateClienteRequest))]
[JsonDerivedType(typeof(CreateDocumentoDigitalRequest), nameof(CreateDocumentoDigitalRequest))]
public abstract class ApiRequestBase
{
    public Guid Id { get; set; }
    public string EmpresaRfc { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool IsProcessed { get; set; }
    public ApiResponseBase? Response { get; set; }
}
