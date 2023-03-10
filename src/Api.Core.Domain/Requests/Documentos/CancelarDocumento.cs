using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CancelarDocumentoRequest : ApiRequestBase, IApiRequest<CancelarDocumentoRequestModel, CancelarDocumentoRequestOptions>
{
    public CancelarDocumentoRequestModel Model { get; set; } = new();
    public CancelarDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class CancelarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
    public string ContrasenaCertificado { get; set; } = string.Empty;
    public string MotivoCancelacion { get; set; } = string.Empty;
    public string Uuid { get; set; } = string.Empty;
}

public sealed class CancelarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    public bool Administrativamente { get; set; } = false;
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CancelarDocumentoResponse : ApiResponseBase, IApiResponse<CancelarDocumentoResponseModel>
{
    public CancelarDocumentoResponseModel Model { get; set; } = new();
}

public sealed class CancelarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
