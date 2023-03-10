using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class ActualizarDocumentoRequest : ApiRequestBase,
    IApiRequest<ActualizarDocumentoRequestModel, ActualizarDocumentoRequestOptions>
{
    public ActualizarDocumentoRequestModel Model { get; set; } = new();
    public ActualizarDocumentoRequestOptions Options { get; set; } = new();
}

public sealed class ActualizarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
    public Dictionary<string, string> DatosDocumento { get; set; } = new();
}

public sealed class ActualizarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class ActualizarDocumentoResponse : ApiResponseBase, IApiResponse<ActualizarDocumentoResponseModel>
{
    public ActualizarDocumentoResponseModel Model { get; set; } = new();
}

public sealed class ActualizarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
