using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class SaldarDocumentoRequest : ApiRequestBase, IApiRequest<SaldarDocumentoRequestModel, SaldarDocumentoRequestOptions>
{
    public SaldarDocumentoRequestModel Model { get; set; } = new();
    public SaldarDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class SaldarDocumentoRequestModel
{
    public LlaveDocumento DocumentoAPagar { get; set; } = new();
    public LlaveDocumento DocumentoPago { get; set; } = new();
    public DateTime Fecha { get; set; } = DateTime.Today;
    public decimal Importe { get; set; }
}

public sealed class SaldarDocumentoRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class SaldarDocumentoResponse : ApiResponseBase, IApiResponse<SaldarDocumentoResponseModel>
{
    public SaldarDocumentoResponseModel Model { get; set; } = new();
}

public sealed class SaldarDocumentoResponseModel
{
    public Documento DocumentoPago { get; set; } = new();
    public Documento DocumentoPagar { get; set; } = new();
}
