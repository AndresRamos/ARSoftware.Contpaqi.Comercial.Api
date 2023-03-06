using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class TimbrarDocumentoRequest : ApiRequestBase, IApiRequest<TimbrarDocumentoRequestModel, TimbrarDocumentoRequestOptions>
{
    public TimbrarDocumentoRequestModel Model { get; set; } = new();
    public TimbrarDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class TimbrarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
    public string ContrasenaCertificado { get; set; } = string.Empty;
}

public sealed class TimbrarDocumentoRequestOptions
{
    public bool AgregarArchivo { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public string ContenidoArchivo { get; set; } = string.Empty;
}

/// <inheritdoc cref="IApiResponse" />
public sealed class TimbrarDocumentoResponse : ApiResponseBase, IApiResponse<TimbrarDocumentoResponseModel>
{
    public TimbrarDocumentoResponseModel Model { get; set; } = new();
}

public sealed class TimbrarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
